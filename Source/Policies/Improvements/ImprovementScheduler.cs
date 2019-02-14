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

namespace Policies.Improvements
{
    public class ImprovementScheduler : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IImprovementPodFactory _improvementPodFactory;
        readonly FactoryFor<IKubernetes> _kubernetesClientFactory;

        public ImprovementScheduler(
            IExecutionContextManager executionContextManager,
            IImprovementPodFactory improvementPodFactory,
            FactoryFor<IKubernetes> kubernetesClientFactory)
        {
            _executionContextManager = executionContextManager;
            _improvementPodFactory = improvementPodFactory;
            _kubernetesClientFactory = kubernetesClientFactory;
        }

        public void Process(FrameworkImprovementRequested @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFrameworkRecipe();
            
            var context = new ImprovementContext(
                _executionContextManager.Current.Tenant,
                null,
                null);

            var pod = _improvementPodFactory.BuildFrom(context, recipe);

            using (var client = _kubernetesClientFactory())
            {
                client.CreateNamespacedPod(pod, pod.Metadata.NamespaceProperty);
            }
        }

    }
}