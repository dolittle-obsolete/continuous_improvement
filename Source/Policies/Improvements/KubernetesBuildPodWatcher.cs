using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Tenancy;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    public class KubernetesBuildPodWatcher
    {
        readonly FactoryFor<IKubernetes> _clientFactory;
        readonly ILogger _logger;
        readonly IExecutionContextManager _executionContextManager;
        readonly FactoryFor<IImprovementStepResultHandler> _stepResultHandlerFactory;

        public KubernetesBuildPodWatcher(
            ILogger logger,
            IExecutionContextManager executionContextManager,
            FactoryFor<IKubernetes> clientFactory,
            FactoryFor<IImprovementStepResultHandler> stepResultHandlerFactory
        )
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _executionContextManager = executionContextManager;
            _stepResultHandlerFactory = stepResultHandlerFactory;
        }


        public void StartWatcher()
        {
            // FIXME: This should run all the time, so have a look at what happens on exceptions and when it is closed. The client should possibly be disposed.
            Task.Run(async () => {
                var client = _clientFactory();
                var watchList = await client.ListNamespacedPodWithHttpMessagesAsync("dolittle-builds", watch: true);
                watchList.Watch<V1Pod>(
                    // OnEvent
                    (eventType, pod) => {
                        _logger.Trace($"Got event {eventType} for pod '{pod.Metadata.Name}'. The status is {pod.Status.Phase}.");

                        // Ignore Pods that have already been deleted
                        if (pod.Metadata.DeletionTimestamp.HasValue) return;

                        if (pod.Status.Phase == "Succeeded")
                        {
                            _logger.Information($"Build-pod '{pod.Metadata}' succeeded.");
                            DeletePod(pod);
                        }
                        else if (pod.Status.Phase == "Failed")
                        {
                            _logger.Warning($"Build-pod '{pod.Metadata}' failed.");
                            ReportBuildStatus(pod);
                            DeletePod(pod);
                        }
                        else
                        {
                            ReportBuildStatus(pod);
                        }
                    },

                    // OnException
                    (ex) => {
                        _logger.Error(ex, "Error while watching list of build pods.");
                    },

                    // OnClose
                    () => {
                        _logger.Error("Build pod watcher was closed unexpectedly.");
                    }
                );
            });
        }
        
        static readonly Regex _stepNameRegex = new Regex(@"^step-(\d+)-(\d+)-?(.*)$", RegexOptions.Compiled);

        void ReportBuildStatus(V1Pod pod)
        {
            if (CheckWarnAndDeleteIfPodIsMissingLabels(pod)) return;

            TenantId tenantId = new Guid(pod.Metadata.Labels[PodLabels.Tenant]);
            RecipeType recipeType = pod.Metadata.Labels[PodLabels.RecipeType];
            ImprovementId improvementId = new Guid(pod.Metadata.Labels[PodLabels.Improvement]);
            ImprovableId improvableId = new Guid(pod.Metadata.Labels[PodLabels.Improvable]);
            VersionString versionString = pod.Metadata.Labels[PodLabels.Version];

            _executionContextManager.CurrentFor(tenantId);
            var stepResultHandler = _stepResultHandlerFactory();

            var buildSteps = new Dictionary<StepNumber, List<StepStatus>>();

            pod.Status.InitContainerStatuses.ForEach(container => {
                var match = _stepNameRegex.Match(container.Name);
                if (match.Success)
                {
                    StepNumber stepNumber = int.Parse(match.Groups[1].Value);
                    //int subStepNumber = int.Parse(match.Groups[2].Value);
                    //var subStepName = match.Groups[3].Value;
                    var exitCode = container.State.Terminated?.ExitCode;

                    var subStepStatus = StepStatus.NotStarted;

                    if (exitCode.HasValue && exitCode != 0) subStepStatus = StepStatus.Failed;
                    else if (exitCode.HasValue) subStepStatus = StepStatus.Succeeded;
                    else if (container.State.Running != null) subStepStatus = StepStatus.InProgress;

                    if (buildSteps.TryGetValue(stepNumber, out var subStepStatuses))
                    {
                        subStepStatuses.Add(subStepStatus);
                    }
                    else
                    {
                        buildSteps.Add(stepNumber, new List<StepStatus>(new [] { subStepStatus } ));
                    }

                }
            });

            buildSteps.ForEach(kv => {
                var stepNumber = kv.Key;
                var subStepStatuses = kv.Value;

                // TODO: These will be called multiple times for each step (at least for successful ones), make sure the state is kept somewhere else!
                if (subStepStatuses.Any(_ => _ == StepStatus.Failed))
                {
                    stepResultHandler.HandleFailedStep(recipeType, stepNumber, improvementId, improvableId, versionString);
                }
                else if (subStepStatuses.All(_ => _ == StepStatus.Succeeded))
                {
                    stepResultHandler.HandleSuccessfulStep(recipeType, stepNumber, improvementId, improvableId, versionString);
                }
            });
        }

        bool CheckWarnAndDeleteIfPodIsMissingLabels(V1Pod pod)
        {
            TenantId tenantId = new Guid(pod.Metadata.Labels[PodLabels.Tenant]);
            RecipeType recipeType = pod.Metadata.Labels[PodLabels.RecipeType];
            ImprovementId improvementId = new Guid(pod.Metadata.Labels[PodLabels.Improvement]);
            ImprovableId improvableId = new Guid(pod.Metadata.Labels[PodLabels.Improvable]);
            VersionString versionString = pod.Metadata.Labels[PodLabels.Version];

            var missing = false;
            missing |= CheckAndWarnIfPodIsMissingLabel(pod, PodLabels.Tenant);
            missing |= CheckAndWarnIfPodIsMissingLabel(pod, PodLabels.RecipeType);
            missing |= CheckAndWarnIfPodIsMissingLabel(pod, PodLabels.Improvement);
            missing |= CheckAndWarnIfPodIsMissingLabel(pod, PodLabels.Improvable);
            missing |= CheckAndWarnIfPodIsMissingLabel(pod, PodLabels.Version);
            
            if (missing) DeletePod(pod);
            return missing;
        }

        bool CheckAndWarnIfPodIsMissingLabel(V1Pod pod, string labelKey)
        {
            if (!pod.Metadata.Labels.ContainsKey(labelKey))
            {
                _logger.Error($"Pod '{pod.Metadata.Name}' in build-pod namespace is missing the '{labelKey}' label. The pod will be deleted.");
                return true;
            }
            return false;
        }

        void DeletePod(V1Pod pod) {
            using (var client = _clientFactory())
            {
                client.DeleteNamespacedPod(new V1DeleteOptions {
                    GracePeriodSeconds = 0,
                    PropagationPolicy = "Foreground",
                }, pod.Metadata.Name, pod.Metadata.NamespaceProperty);
            }
        }
    }
}