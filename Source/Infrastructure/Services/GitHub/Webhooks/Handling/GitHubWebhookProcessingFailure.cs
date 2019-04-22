/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Indicates that there has been an exception in processing a github webhook
    /// </summary>
    [Serializable]
    public class GitHubWebHookProcessingFailure : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the GitHubWebHookProcessingFailure custom exception
        /// </summary>
        public GitHubWebHookProcessingFailure()
        {}

        /// <summary>
        ///     Initializes a new instance of the GitHubWebHookProcessingFailure custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public GitHubWebHookProcessingFailure(string message)
            : base(message)
        {}

        /// <summary>
        ///     Initializes a new instance of the GitHubWebHookProcessingFailure custom exception
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        /// <param name="innerException">An inner exception that is the original source of the error</param>
        public GitHubWebHookProcessingFailure(string message, Exception innerException)
            : base(message, innerException)
        {}

        /// <summary>
        ///     Initializes a new instance of the GitHubWebHookProcessingFailure custom exception
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the object data of the exception</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination</param>
        protected GitHubWebHookProcessingFailure(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}
    }
}