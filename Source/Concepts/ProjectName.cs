/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts
{
    /// <summary>
    /// Represents the name of a project
    /// </summary>
    public class ProjectName : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="ProjectName"/>
        /// </summary>
        /// <param name="name"><see cref="string"/> to convert from</param>
        public static implicit operator ProjectName(string name)
        {
            return new ProjectName { Value = name };
        }
    }
}