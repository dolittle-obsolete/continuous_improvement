/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts.Configuration
{
    /// <summary>
    /// Represents the unique identifier for an deployment in the system
    /// </summary>
    public class DeploymentId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="DeploymentId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> to convert from</param>
        public static implicit operator DeploymentId(Guid value)
        {
            return new DeploymentId { Value = value };
        }
    }
}