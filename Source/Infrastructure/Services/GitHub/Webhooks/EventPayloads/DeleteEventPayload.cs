
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
    ///  Represents the payload for a delete event
    /// </summary>
    public class DeleteEventPayload : ActivityPayload
    {
        /// <summary>
        /// The Ref
        /// </summary>
        public string Ref { get; protected set; }
        /// <summary>
        /// The Ref Type
        /// </summary>
        /// <value></value>
        public string RefType { get; protected set; }

    }
}
#pragma warning restore 1591