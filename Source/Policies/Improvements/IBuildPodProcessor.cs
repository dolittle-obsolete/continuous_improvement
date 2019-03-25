/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a processor for a build pod
    /// </summary>
    public interface IBuildPodProcessor
    {
        /// <summary>
        /// Processes an <see cref="IPod" />
        /// </summary>
        void Process(IPod pod);
    }
}