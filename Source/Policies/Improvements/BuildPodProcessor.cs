/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Runtime.Tenancy;
using Policies.Improvements.StepHandling;
using Policies.Improvements.Tracking;

namespace Policies.Improvements
{
    /// <inheritdoc />
    public class BuildPodProcessor : IBuildPodProcessor
    {
        private readonly ILogger _logger;
        private readonly IHandleBuildSteps _handleBuildSteps;
        private readonly FactoryFor<IBuildStepsStatusTracker> _getTracker;
        private readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Instantiates an instance of <see cref="BuildPodProcessor" />
        /// </summary>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" /> for setting the correct <see cref="Tenant"/> context</param>
        /// <param name="handleBuildSteps">A <see cref="IHandleBuildSteps" /> for handling each build step</param>
        /// <param name="getTracker">A factory for building instances of <see cref="IBuildStepsStatusTracker" /> for tracking the status of individual build steps</param>
        /// <param name="logger">A logger for logging</param>
        public BuildPodProcessor(IExecutionContextManager executionContextManager, IHandleBuildSteps handleBuildSteps, FactoryFor<IBuildStepsStatusTracker> getTracker, ILogger logger)
        {
            _logger = logger;
            _handleBuildSteps = handleBuildSteps;
            _getTracker = getTracker;
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc />
        public void Process(IPod pod)
        {  
            _executionContextManager.CurrentFor(pod.Metadata.Tenant); 
            if(pod.IsDeleted)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' is deleted.");
                return;
            } 

            if(!pod.HasBuildContainerStatuses)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' has no statuses to process.");
                return;
            } 

            if (pod.HasSucceeded)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' succeeded.");
                ProcessSteps(pod);
                pod.Delete();
                return;
            }
            else if (pod.HasFailed)
            {
                _logger.Warning($"Build-pod '{pod.Metadata.ToString()}' failed.");
                pod.Delete();
                return;
            }
            _logger.Information($"Build-pod '{pod.Metadata.ToString()}' still in progress.");
            ProcessSteps(pod);
        }

        void ProcessSteps(IPod pod)
        {
            var tracker = _getTracker();
            pod.Statuses.ForEach(_ => tracker.Track(_));
            _handleBuildSteps.Handle(pod.Metadata,tracker);
        }
    }
}