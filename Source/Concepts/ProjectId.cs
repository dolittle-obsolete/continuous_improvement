/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts
{
    /// <summary>
    /// Represents the concept of a unique identifier for a project
    /// </summary>
    public class ProjectId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="ProjectId"/>
        /// </summary>
        /// <param name="id"><see cref="Guid"/> to convert from</param>
        public static implicit operator ProjectId(Guid id)
        {
            return new ProjectId { Value = id };
        }
    }
}