/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Runtime.Events;
using Events.Improvements;
using k8s;
using k8s.Models;
using Policies.Improvements.Recipes;
using Read.Improvables;
using Read.Improvements;

namespace Policies.Improvements
{
    /// <summary>
    /// Schedules improvements with Kubernetes when receiving an <see cref="ImprovementInitiated" /> event
    /// </summary>
    public class ImprovementScheduler : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IImprovementPodFactory _improvementPodFactory;
        readonly FactoryFor<IKubernetes> _kubernetesClientFactory;
        readonly IImprovableManager _improvableManager;

        /// <summary>
        /// Instantiates an instance of <see cref="ImprovementScheduler" />
        /// </summary>
        /// <param name="executionContextManager">An <see cref="IExecutionContextManager" /> for accessing the current <see cref="ExecutionContext" /></param>
        /// <param name="improvementPodFactory">An <see cref="IImprovementPodFactory" /> for creating <see cref="V1Pod">improvement pods</see></param>
        /// <param name="improvableManager">An <see cref="IImprovableManager" /> for fetching an <see cref="Improvable" /></param>
        /// <param name="kubernetesClientFactory">A factory for creating <see cref="IKubernetes">kubernetes client</see></param>
        public ImprovementScheduler(
            IExecutionContextManager executionContextManager,
            IImprovementPodFactory improvementPodFactory,
            IImprovableManager improvableManager,
            FactoryFor<IKubernetes> kubernetesClientFactory)
        {
            _executionContextManager = executionContextManager;
            _improvementPodFactory = improvementPodFactory;
            _kubernetesClientFactory = kubernetesClientFactory;
            _improvableManager = improvableManager;
        }

        /// <inheritdoc />
        [EventProcessor("dda99c89-5659-441b-8026-e013d95aa732")]
        public void Process(ImprovementInitiated @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFramework();
            var improvement = new Improvement
            {
                Id = eventSourceId.Value,
                Improvable = @event.ForImprovable,
                PullRequest = @event.PullRequest,
                Version = @event.Version
            };

            var improvable = _improvableManager.GetById(@event.ForImprovable);
            
            var context = new ImprovementContext(
                _executionContextManager.Current.Tenant,
                improvement,
                improvable);

            var pod = _improvementPodFactory.BuildFrom(context, recipe);

            using (var client = _kubernetesClientFactory())
            {
                client.CreateNamespacedPod(pod, pod.Metadata.NamespaceProperty);
            }
        }
    }
}