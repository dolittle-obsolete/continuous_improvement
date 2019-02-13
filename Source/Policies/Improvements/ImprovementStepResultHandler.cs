/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Execution;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Commands.Coordination;
using Domain.Improvements;

namespace Policies.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class ImprovementStepResultHandler : IImprovementStepResultHandler
    {
        static ArtifactId _nullCommandArtifactId = (ArtifactId)Guid.Parse("c7d1f5cc-40bb-4cd4-b589-9cb11a43c962");

        readonly ICommandContextManager _commandContextManager;
        readonly IAggregateRootRepositoryFor<Improvement> _repository;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandContextManager"></param>
        /// <param name="repository"></param>
        /// <param name="improvementPodFactory"></param>
        public ImprovementStepResultHandler(
            IExecutionContextManager executionContextManager,
            ICommandContextManager commandContextManager,
            IAggregateRootRepositoryFor<Improvement> repository)
        {
            _commandContextManager = commandContextManager;
            _repository = repository;
            _executionContextManager = executionContextManager;
        }

        public void HandleSuccessfulStep(
            StepNumber stepNumber,
            ImprovementId improvement,
            ImprovableId improvable,
            VersionString version)
        {

        }
        
        public void HandleFailedStep(
            StepNumber stepNumber,
            ImprovementId improvement,
            ImprovableId improvable,
            VersionString version)
        {

        }

/*
        public void HandleSuccessfulStep(ImprovableId improvable, VersionString version, StepNumber stepNumber)
        {
            var events = step.GetSucceededEventsFor(context);
            ApplyEventsFor(context, events);
        }

        public void HandleFailedStep(ImprovableId improvable, VersionString version, StepNumber stepNumber)
        {
            var events = step.GetFailedEventsFor(context);
            ApplyEventsFor(context, events);
        }*/

        void ApplyEventsFor(ImprovementContext context, IEnumerable<IEvent> events)
        {
           var request = new CommandRequest(_executionContextManager.Current.CorrelationId, _nullCommandArtifactId, ArtifactGeneration.First, new Dictionary<string,object>());
            
            using( var commandContext = _commandContextManager.EstablishForCommand(request) )
            {
                var improvement = _repository.Get(context.Improvement.Id);
                events.ForEach(_ => improvement.Apply(_));
            }
        }
    }
}