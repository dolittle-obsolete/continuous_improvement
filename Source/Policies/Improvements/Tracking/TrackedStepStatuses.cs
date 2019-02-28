/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvements;

namespace Policies.Improvements.Tracking
{

    public class TrackedStepStatuses
    {
        public TrackedStepStatuses(StepNumber step, IEnumerable<StepStatus> statuses)
        {
            Step = step;
            Statuses = statuses;
        }

        public StepNumber Step { get; }
        public IEnumerable<StepStatus> Statuses { get; }
    }
}