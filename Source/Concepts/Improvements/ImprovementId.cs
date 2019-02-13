/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;
using Dolittle.Runtime.Events;

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents the unique identifier for an improvable in the system
    /// </summary>
    public class ImprovementId : EventSourceId
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="ImprovementId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> to convert from</param>
        public static implicit operator ImprovementId(Guid value)
        {
            return new ImprovementId { Value = value };
        }
    }
}