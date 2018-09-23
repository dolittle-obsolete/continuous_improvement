/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// The <see cref="Exception"/> that gets thrown when a <see cref="IPerformer{T}"/> needs configuration and is not given it
    /// </summary>
    public class PerformerNeedsConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PerformerNeedsConfiguration"/>
        /// </summary>
        /// <param name="type">Type of <see cref="IPerformer{T}"/></param>
        public PerformerNeedsConfiguration(Type type) : base($"Performer of type '{type.AssemblyQualifiedName}' needs configuration but was not given it")
        {

        }

    }
}