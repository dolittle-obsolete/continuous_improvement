/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    /// <summary>
    /// Records that a Step failed
    /// </summary>
    public class StepFailed : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="StepFailed" />
        /// </summary>
        /// <param name="stepNumber">The Step Number that failed</param>
        public StepFailed(int stepNumber) => StepNumber = stepNumber;
        /// <summary>
        /// The Step Number that failed
        /// </summary>
        public int StepNumber { get; }
    }
}