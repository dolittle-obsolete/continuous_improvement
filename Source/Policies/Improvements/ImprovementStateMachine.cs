/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Runtime.Events;

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
        public FrameworkImprovementRequested(VersionString version) {}

        /// <inheritdoc/>
        public string Version {  get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ImprovementStateMachine : ICanProcessEvents
    {
        private readonly ICommandContextManager _commandContextManager;
        private readonly IAggregateRootRepositoryFor<Improvement> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandContextManager"></param>
        /// <param name="repository"></param>
        public ImprovementStateMachine(
            ICommandContextManager commandContextManager,
            IAggregateRootRepositoryFor<Improvement> repository)
        {
            _commandContextManager = commandContextManager;
            _repository = repository;
        }

        /// <summary>
        /// Processes
        /// </summary>
        public void Process(FrameworkImprovementRequested @event)
        {

        }
    }
}