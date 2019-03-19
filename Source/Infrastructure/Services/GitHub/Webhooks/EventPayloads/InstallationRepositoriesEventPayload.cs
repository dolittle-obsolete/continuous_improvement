/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Octokit;

#pragma warning disable 1591
namespace Infrastructure.Services.Github.Webhooks.EventPayloads
{
    /// <summary>
    /// Represents the payload for an installation reposoitories event
    /// </summary>
    public class InstallationRepositoriesEventPayload : ActivityPayload
    {
        /// <summary>
        /// The action type
        /// </summary>
        public string Action { get; protected set; }
        /// <summary>
        /// The repository selection
        /// </summary>
        public string RepositorySelection { get; protected set; }
        /// <summary>
        /// A list of the repositories that have been added
        /// </summary>
        public IReadOnlyList<Repository> RepositoriesAdded { get; protected set; }
        /// <summary>
        /// A list of repositories that have been removed
        /// </summary>
        public IReadOnlyList<Repository> RepositoriesRemoved { get; protected set; }
    }
}
#pragma warning restore 1591