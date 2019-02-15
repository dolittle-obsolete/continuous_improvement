/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{

    public class ImprovementFailed : IEvent 
    {
        public ImprovementFailed(Guid forImprovable)
        {
            ForImprovable = forImprovable;
        }
        /// Not technically needed but should make things easier for now
        public Guid ForImprovable { get; }
    }
}