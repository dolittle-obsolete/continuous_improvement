/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents the status of a step
    /// </summary>
    public enum StepStatus 
    {
        /// <summary>
        /// Step has not started
        /// </summary>
        NotStarted=1,

        /// <summary>
        /// Step is in progress
        /// </summary>
        InProgress,

        /// <summary>
        /// Step failed
        /// </summary>
        Failed,

        /// <summary>
        /// Step succeeded
        /// </summary>
        Succeeded,
    }
}