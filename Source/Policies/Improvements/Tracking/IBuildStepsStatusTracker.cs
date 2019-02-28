/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvements;

namespace Policies.Improvements.Tracking
{
    /// <summary>
    /// Tracks the status of all build steps (and substeps )
    /// </summary>
    public interface IBuildStepsStatusTracker : IEnumerable<TrackedStepStatuses>
    {
        /// <summary>
        /// Adds a Build Step and status to track
        /// </summary>
        /// <param name="stepNumber">The identifier of the Build Step</param>
        /// <param name="status">The status to append to the Build Step</param>
        void Track(StepNumber stepNumber, StepStatus status);
    }
}