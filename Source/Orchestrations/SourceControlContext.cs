/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Orchestrations
{
    /// <summary>
    /// Represents the context for source control
    /// </summary>
    public class SourceControlContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SourceControlContext"/>
        /// </summary>
        /// <param name="repository">The <see cref="Uri">url</see> for the repository</param>
        /// <param name="commit">The key representing the commit</param>
        /// <param name="isPullRequest">Whether or not this is a pull request</param>
        /// <remarks>
        /// Commit is typically the SHA in Git for instance - currently Git is the only supported
        /// source control provider
        /// </remarks>
        public SourceControlContext(
            Uri repository,
            string commit,
            bool isPullRequest)
        {
            Repository = repository;
            Commit = commit;
            IsPullRequest = isPullRequest;
        }

        /// <summary>
        /// Gets the <see cref="Uri">url</see> for the repository
        /// </summary>
        public Uri Repository { get; }

        /// <summary>
        /// Gets the key representing the commit
        /// </summary>
        public string Commit { get; }

        /// <summary>
        /// Get whether or not the commit is a pull request
        /// </summary>
        public bool IsPullRequest { get; }
    }
}