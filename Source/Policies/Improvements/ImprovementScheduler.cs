/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Runtime.Events;
using Events.Improvements;
using Policies.Improvements.Recipes;

namespace Policies.Improvements
{
    public class ImprovementScheduler : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IImprovementPodFactory _improvementPodFactory;

        public ImprovementScheduler(
            IExecutionContextManager executionContextManager,
            IImprovementPodFactory improvementPodFactory)
        {
            _executionContextManager = executionContextManager;
            _improvementPodFactory = improvementPodFactory;
        }

        public void Process(ImprovementRequested @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFramework();
            @event.Improvable
            
            var context = new ImprovementContext(
                _executionContextManager.Current.Tenant,
                null,
                null);

            var pod = _improvementPodFactory.BuildFrom(context, recipe);

            // Schedule on K8s
        }

    }
}