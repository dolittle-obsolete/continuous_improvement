namespace Domain.Improvements.Metadata
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the sitaution where the metadata describing the improvement is invalid
    /// </summary>
    [Serializable]
    public class InvalidImprovementMetadata : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the InvalidImprovementMetadata custom exception
        /// </summary>
        public InvalidImprovementMetadata()
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidImprovementMetadata custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public InvalidImprovementMetadata(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidImprovementMetadata custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public InvalidImprovementMetadata(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the InvalidImprovementMetadata custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected InvalidImprovementMetadata(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}