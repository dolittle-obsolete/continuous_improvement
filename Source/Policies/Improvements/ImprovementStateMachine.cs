/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Lifecycle;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandContextManager"></param>
        /// <param name="repository"></param>
        /// <param name="improvementPodFactory"></param>
        public ImprovementStateMachine(
            ICommandContextManager commandContextManager,
            IAggregateRootRepositoryFor<Improvement> repository,
            IImprovementPodFactory improvementPodFactory)
        {
            _commandContextManager = commandContextManager;
            _repository = repository;
            _improvementPodFactory = improvementPodFactory;
        }

        /// <summary>
        /// Processes
        /// </summary>
        public void Process(FrameworkImprovementRequested @event, EventSourceId eventSourceId)
        {
            var recipe = new DotNetFrameworkRecipe();
            

        }
    }
}