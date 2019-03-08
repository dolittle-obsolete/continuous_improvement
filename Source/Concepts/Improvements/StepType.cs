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
    public class StepType : ConceptAs<Guid>
    {
        /// <summary>
        /// An empty / not set Id
        /// </summary>
        public static StepType Empty { get; } = Guid.Empty;

        /// <summary>
        /// Initializes a new instance of <see cref="StepType"/>
        /// </summary>
        public StepType() 
        { 
            Value = Guid.Empty;
        }


        /// <summary>
        /// Instantiates an instance of an <see cref="StepType" /> with the specified value
        /// </summary>
        /// <param name="value"></param>
        public StepType(Guid value) => Value = value;
        

        /// <summary>
        /// Implicitly convert Guid to an StepType
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator StepType(Guid value) => new StepType(value);
    }
}