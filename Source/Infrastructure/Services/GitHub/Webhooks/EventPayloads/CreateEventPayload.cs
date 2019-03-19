
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
    /// Represents the payload for a create event
    /// </summary>
    public class CreateEventPayload : ActivityPayload
    {
        /// <summary>
        /// The Ref
        /// </summary>
        public string Ref { get; protected set; }
        /// <summary>
        /// The Ref type
        /// </summary>
        public string RefType { get; protected set; }
        /// <summary>
        /// The name of the master branch
        /// </summary>
        public string MasterBranch { get; protected set; }
        /// <summary>
        /// A description
        /// </summary>
        /// <value></value>
        public string Description { get; protected set; }

    }
}
#pragma warning restore 1591