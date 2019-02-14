/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    public class StepSucceeded : IEvent
    {
        public StepSucceeded(int stepNumber)
        {
            StepNumber = stepNumber;
        }

        public int StepNumber { get; }
    }
}