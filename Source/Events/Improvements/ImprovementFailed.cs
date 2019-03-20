/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{

    /// <summary>
    /// Records that an Improvement has Failed
    /// </summary>
    public class ImprovementFailed : IEvent 
    {
        /// <summary>
        /// Instantiates an a new instance of <see cref="ImprovementFailed" />
        /// </summary>
        /// <param name="forImprovable">The Id of the Improvable that this improvement is for</param>
        public ImprovementFailed(Guid forImprovable)
        {
            ForImprovable = forImprovable;
        }

        /// <summary>
        /// The Id of the Improvable that this improvement is for
        /// Not technically needed but should make things easier for now
        /// </summary>
        public Guid ForImprovable { get; }
    }
}