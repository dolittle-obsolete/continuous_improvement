/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;
using Dolittle.Runtime.Events;

namespace Concepts.Frameworks
{
    /// <summary>
    /// Encapsulates a Unique Identifier
    /// </summary>
    /// <typeparam name="Guid"></typeparam>
    public class FrameworkId : ConceptAs<Guid>
    {
        /// <summary>
        /// An empty / not set Id
        /// </summary>
        public static FrameworkId Empty { get; } = Guid.Empty;

        /// <summary>
        /// Instantiates an instance of an <see cref="FrameworkId" /> with the specified value
        /// </summary>
        /// <param name="value"></param>
        public FrameworkId(Guid value) => Value = value;
        
        /// <summary>
        /// Create an instance of an <see cref="FrameworkId" /> with a generated value
        /// </summary>
        /// <returns></returns>
        public static FrameworkId New() => Guid.NewGuid();   

        /// <summary>
        /// Implicitly convert Guid to an FrameworkId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FrameworkId(Guid value) => new FrameworkId(value);
        
        /// <summary>
        /// Implicitly convert EventSourceId to an FrameworkId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FrameworkId(EventSourceId value) => new FrameworkId(value);

        /// <summary>
        /// Implicitly convert ImprovmentId to an EventSourceId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator EventSourceId(FrameworkId value) => new EventSourceId(value);
    }
}