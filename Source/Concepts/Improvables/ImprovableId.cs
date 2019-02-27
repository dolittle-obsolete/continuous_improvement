/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;
using Dolittle.Runtime.Events;

namespace Concepts.Improvables
{
    /// <summary>
    /// Encapsulates a Unique Identifier
    /// </summary>
    /// <typeparam name="Guid"></typeparam>
    public class ImprovableId : ConceptAs<Guid>
    {
        /// <summary>
        /// An empty / not set Id
        /// </summary>
        public static ImprovableId Empty { get; } = Guid.Empty;

        /// <summary>
        /// Instantiates an instance of an <see cref="ImprovableId" /> with the specified value
        /// </summary>
        /// <param name="value"></param>
        public ImprovableId(Guid value) => Value = value;
        
        /// <summary>
        /// Create an instance of an <see cref="ImprovableId" /> with a generated value
        /// </summary>
        /// <returns></returns>
        public static ImprovableId New() => Guid.NewGuid();   

        /// <summary>
        /// Implicitly convert Guid to an ImprovableId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ImprovableId(Guid value) => new ImprovableId(value);
        
        /// <summary>
        /// Implicitly convert EventSourceId to an ImprovableId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ImprovableId(EventSourceId value) => new ImprovableId(value);

        /// <summary>
        /// Implicitly convert ImprovmentId to an EventSourceId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator EventSourceId(ImprovableId value) => new EventSourceId(value);
    }
}