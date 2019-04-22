/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


namespace Domain.Improvables
{
    using System;
    using System.Runtime.Serialization;
    
    /// <summary>
    /// Indicates the exceptional situation where there is an attempt to register an improvable
    /// that has already been registered
    /// </summary>
    [Serializable]
    public class ImprovableAlreadyRegistered : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the ImprovableAlreadyRegistered custom exception
        /// </summary>
        public ImprovableAlreadyRegistered()
        {}
    
        /// <summary>
        ///     Initializes a new instance of the ImprovableAlreadyRegistered custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public ImprovableAlreadyRegistered(string message)
            : base(message)
        {}
    
        /// <summary>
        ///     Initializes a new instance of the ImprovableAlreadyRegistered custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public ImprovableAlreadyRegistered(string message, Exception innerException)
            : base(message, innerException)
        {}
    
        /// <summary>
        ///     Initializes a new instance of the ImprovableAlreadyRegistered custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected ImprovableAlreadyRegistered(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}