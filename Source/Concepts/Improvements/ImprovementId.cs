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
    /// Encapsulates a Unique Identifier
    /// </summary>
    /// <typeparam name="Guid"></typeparam>
    public class ImprovementId : ConceptAs<Guid>
    {
        /// <summary>
        /// An empty / not set Id
        /// </summary>
        public static ImprovementId Empty { get; } = Guid.Empty;

        /// <summary>
        /// Instantiates an instance of an <see cref="ImprovementId" /> with the specified value
        /// </summary>
        /// <param name="value"></param>
        public ImprovementId(Guid value) => Value = value;
        
        /// <summary>
        /// Create an instance of an <see cref="ImprovementId" /> with a generated value
        /// </summary>
        /// <returns></returns>
        public static ImprovementId New() => Guid.NewGuid();   

        /// <summary>
        /// Implicitly convert Guid to an ImprovementId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ImprovementId(Guid value) => new ImprovementId(value);
        
        /// <summary>
        /// Implicitly convert EventSourceId to an ImprovementId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ImprovementId(EventSourceId value) => new ImprovementId(value);

        /// <summary>
        /// Implicitly convert ImprovmentId to an EventSourceId
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator EventSourceId(ImprovementId value) => new EventSourceId(value);
    }
}