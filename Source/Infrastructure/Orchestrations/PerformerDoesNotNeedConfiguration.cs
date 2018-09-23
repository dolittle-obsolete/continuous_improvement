/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// The <see cref="Exception"/> that gets thrown when a <see cref="IPerformer{T}"/> does not need configuration but is still given one
    /// </summary>
    public class PerformerDoesNotNeedConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PerformerDoesNotNeedConfiguration"/>
        /// </summary>
        /// <param name="type">Type of <see cref="IPerformer{T}"/></param>
        public PerformerDoesNotNeedConfiguration(Type type) : base($"Performer of type '{type.AssemblyQualifiedName}' does not need configuration but was given it")
        {

        }

    }
}