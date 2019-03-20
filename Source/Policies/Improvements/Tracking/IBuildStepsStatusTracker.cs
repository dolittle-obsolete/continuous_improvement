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
        /// <param name="containerStepStatus">The step container to track</param>
        void Track(IContainerStatus containerStepStatus);
    }
}