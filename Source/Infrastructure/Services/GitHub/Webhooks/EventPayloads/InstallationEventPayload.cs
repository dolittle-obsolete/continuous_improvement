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
    /// Represents the payload for an installation event
    /// </summary>
    public class InstallationEventPayload : ActivityPayload
    {
        /// <summary>
        /// The action type
        /// </summary>
        public string Action { get; protected set; }
        /// <summary>
        /// A list of repositories associated with the installation
        /// </summary>

        public IReadOnlyList<Repository> Repositories { get; protected set; }
    }
}
#pragma warning restore 1591