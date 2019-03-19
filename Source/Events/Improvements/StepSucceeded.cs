/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    /// <summary>
    /// Records that a Step Succeeded
    /// </summary>
    public class StepSucceeded : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="StepSucceeded" />
        /// </summary>
        /// <param name="stepNumber">The step that succeeded</param>
        public StepSucceeded(int stepNumber)
        {
            StepNumber = stepNumber;
        }

        /// <summary>
        /// The Step that succeeded
        /// </summary>
        public int StepNumber { get; }
    }
}