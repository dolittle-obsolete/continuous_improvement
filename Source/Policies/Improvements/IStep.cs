/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts;
using Concepts.Improvements;
using Dolittle.Events;
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines the abstracts of a step
    /// </summary>
    public interface IStep
    {
        /// <summary>
        /// Gets the type of step
        /// </summary>
        StepType Type { get; }

        /// <summary>
        /// Get the containers to run in the step
        /// </summary>
        /// <param name="number"><see cref="StepNumber">Number</see> for the step</param>
        /// <param name="context"><see cref="ImprovementContext">Context</see> for the improvement</param>
        /// <returns><see cref="V1Container">Containers</see> to schedule</returns>
        IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context);

        /// <summary>
        /// Get the name of the log parser to use for the step
        /// </summary>
        /// <param name="number"><see cref="StepNumber">Number</see> for the step</param>
        /// <param name="context"><see cref="ImprovementContext">Context</see> for the improvement</param>
        /// <returns><see cref="LogParserName">Log parser</see> to use</returns>
        LogParserName GetLogParserNameFor(StepNumber number, ImprovementContext context);

        /// <summary>
        /// This method gets called when the step has succeeded - its responsibility then is to provide the
        /// necessary events to apply to the <see cref="EventSource"/>
        /// </summary>
        /// <param name="context"><see cref="ImprovementContext">Context</see> for the improvement</param>
        /// <returns><see cref="IEvent">Events</see> to apply</returns>
        IEnumerable<IEvent> GetSucceededEventsFor(ImprovementContext context);

        /// <summary>
        /// This method gets called when the step has failed - its responsibility then is to provide the
        /// necessary events to apply to the <see cref="EventSource"/>
        /// </summary>
        /// <param name="context"><see cref="ImprovementContext">Context</see> for the improvement</param>
        /// <returns><see cref="IEvent">Events</see> to apply</returns>
        IEnumerable<IEvent> GetFailedEventsFor(ImprovementContext context);
    }
}