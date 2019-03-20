/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Concepts.Improvables
{
    /// <summary>
    /// Represents the status of an Improvable
    /// </summary>
    public enum ImprovableStatus
    {
        /// <summary>
        /// The Improvable succeeded
        /// </summary>
        Success=1,
        /// <summary>
        /// The Improvable is still in progress
        /// </summary>
        InProgress,
        /// <summary>
        /// The Improvable failed
        /// </summary>
        Failed
    }
}