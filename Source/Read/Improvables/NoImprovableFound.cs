/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Read.Improvables
{
    /// <summary>
    /// Error when the expected improvable cannot be found
    /// </summary>
    [Serializable]
    public class NoImprovableFound : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the NoImprovableFound custom exception
        /// </summary>
        public NoImprovableFound()
        {}

        /// <summary>
        ///     Initializes a new instance of the NoImprovableFound custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public NoImprovableFound(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the NoImprovableFound custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public NoImprovableFound(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the NoImprovableFound custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected NoImprovableFound(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }

}