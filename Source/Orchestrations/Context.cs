/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents the continuous improvement context 
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>
        /// </summary>
        /// <param name="project"><see cref="Project"/> configuration</param>
        /// <param name="source">The path to the source folder</param>
        /// <param name="commit"><see cref="string">Commit</see> to build</param>
        /// <remarks>
        /// Commit is typically the SHA in Git for instance - currently Git is the only supported
        /// source control provider
        /// </remarks>
        public Context(
            Project project,
            string source,
            string commit,
            int buildNumber,
            bool isPullRequest)
        {
            Project = project;
            Source = source;
            Commit = commit;
            BuildNumber = buildNumber;
            IsPullRequest = isPullRequest;
        }

        /// <summary>
        /// Gets the <see cref="Project"/> configuration object
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets the path to the folder where the source is located
        /// </summary>
        /// <value></value>
        public string Source { get; }

        /// <summary>
        /// Gets the commit that triggered the build
        /// </summary>
        public string Commit { get; }

        /// <summary>
        /// Gets the build number for the context
        /// </summary>
        public int BuildNumber { get; }

        /// <summary>
        /// Gets whether or not the context represents a pull request 
        /// </summary>
        public bool IsPullRequest { get; }

        /// <summary>
        /// Gets or sets the  version for the context
        /// </summary>
        public string Version { get; set;  } = "1.0.0";
    }
}