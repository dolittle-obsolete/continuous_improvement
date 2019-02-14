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
using Policies.Improvements.Recipes;
using Read.Improvables;
using Read.Improvements;

namespace Policies.Improvements
{
    public class ImprovementScheduler : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IImprovementPodFactory _improvementPodFactory;
        readonly FactoryFor<IKubernetes> _kubernetesClientFactory;
        readonly IImprovableManager _improvableManager;

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

        public void Process(ImprovementRequested @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFramework();
            var improvement = new Improvement
            {
                Id = eventSourceId.Value,
                Improvable = @event.Improvable,
                PullRequest = @event.PullRequest,
                Version = @event.Version
            };

            var improvable = _improvableManager.GetById(@event.Improvable);
            
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