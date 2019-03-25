/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Concepts.Improvements;

namespace Policies.Improvements.Tracking
{
    /// <summary>
    /// Encapsulates and summarises <see cref="StepStatus">step statuses'</see> data for a step
    /// </summary>
    public class TrackedStepStatuses
    {
        /// <summary>
        /// Instantiate an instance of 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="statuses"></param>
        public TrackedStepStatuses(StepNumber step, IEnumerable<StepStatus> statuses)
        {
            Step = step;
            Statuses = statuses;
        }
        /// <summary>
        /// The Step being tracked
        /// </summary>
        public StepNumber Step { get; }
        /// <summary>
        /// <see cref="StepStatus">Statuses</see> of the Step being tracked
        /// </summary>
        public IEnumerable<StepStatus> Statuses { get; }
        /// <summary>
        /// Calculates if the Step has failed
        /// </summary>
        public bool HasFailed => Statuses.Any(_ => _ == StepStatus.Failed);
        /// <summary>
        /// Calculates if the Step has succeeded
        /// </summary>
        public bool HasSucceeded => Statuses.All(_ => _ == StepStatus.Succeeded) && Statuses.Any();
        /// <summary>
        /// Indicates if the Step has been handled
        /// </summary>
        public bool HasBeenHandled { get; private set; }
        /// <summary>
        /// Marks the Step as having been handled
        /// </summary>
        public void MarkAsHandled()
        {
            HasBeenHandled = true;
        }
    }
}