/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts.Improvables
{
    /// <summary>
    /// Represents the unique identifier for an improvable in the system
    /// </summary>
    public class ImprovableId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="ImprovableId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> to convert from</param>
        public static implicit operator ImprovableId(Guid value)
        {
            return new ImprovableId { Value = value };
        }
    }
}