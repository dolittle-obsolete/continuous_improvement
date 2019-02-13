/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Events;

namespace Events.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameworkImprovementRequested : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        public FrameworkImprovementRequested(string version) {}

        /// <inheritdoc/>
        public string Version {  get; }
    }
}