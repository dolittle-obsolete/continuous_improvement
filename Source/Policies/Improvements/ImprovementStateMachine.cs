/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Runtime.Events;
using Policies.Improvements.Recipes;

namespace Policies.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class Improvement : AggregateRoot
    {
        /// <inheritdoc/>
        Improvement(EventSourceId id) : base(id) {}

    }

    /// <summary>
    /// 
    /// </summary>
    public class FrameworkImprovementRequested : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        public FrameworkImprovementRequested(string version) {}

        /// <inheritdoc/>
        public string Version {  get; }
    }



    /// <summary>
    /// 
    /// </summary>
    [Singleton]
    public class ImprovementStateMachine : ICanProcessEvents
    {
        readonly ICommandContextManager _commandContextManager;
        readonly IAggregateRootRepositoryFor<Improvement> _repository;
        readonly IImprovementPodFactory _improvementPodFactory;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandContextManager"></param>
        /// <param name="repository"></param>
        /// <param name="improvementPodFactory"></param>
        public ImprovementStateMachine(
            IExecutionContextManager executionContextManager,
            ICommandContextManager commandContextManager,
            IAggregateRootRepositoryFor<Improvement> repository,
            IImprovementPodFactory improvementPodFactory)
        {
            _commandContextManager = commandContextManager;
            _repository = repository;
            _improvementPodFactory = improvementPodFactory;
            _executionContextManager = executionContextManager;
        }

        /// <summary>
        /// Processes
        /// </summary>
        public void Process(FrameworkImprovementRequested @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFrameworkRecipe();
        }

        static ArtifactId _nullCommandArtifactId = (ArtifactId)Guid.Parse("c7d1f5cc-40bb-4cd4-b589-9cb11a43c962");

        void HandleSucceededStep(ImprovementContext context, IStep step)
        {
            var events = step.GetSucceededEventsFor(context);
            ApplyEventsFor(context, events);
        }

        void HandleFailedStep(ImprovementContext context, IStep step)
        {
            var events = step.GetFailedEventsFor(context);
            ApplyEventsFor(context, events);
        }

        void ApplyEventsFor(ImprovementContext context, IEnumerable<IEvent> events)
        {
           var request = new CommandRequest(_executionContextManager.Current.CorrelationId, _nullCommandArtifactId, ArtifactGeneration.First, new Dictionary<string,object>());
            
            using( var commandContext = _commandContextManager.EstablishForCommand(request) )
            {
                var improvement = _repository.Get(context.Improvement);
                events.ForEach(_ => improvement.Apply(_));
            }
        }
    }
}