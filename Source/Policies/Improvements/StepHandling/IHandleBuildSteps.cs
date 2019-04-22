/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Policies.Improvements.Tracking;

namespace Policies.Improvements.StepHandling
{
    /// <summary>
    /// Defines a handler for build steps
    /// </summary>
    public interface IHandleBuildSteps
    {
        /// <summary>
        /// Handles a build step
        /// </summary>
        /// <param name="improvementMetadata">Metadata for this Improvement</param>
        /// <param name="steps">A collection of <see cref="TrackedStepStatuses">step statuses that are tracked</see></param>
        void Handle(Domain.Improvements.Metadata.ImprovementMetadata improvementMetadata, IEnumerable<TrackedStepStatuses> steps);
    }
}