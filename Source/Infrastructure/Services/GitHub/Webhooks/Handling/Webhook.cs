/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// Incapsulates the Webhook Handler and Payload for scheduling and processing
    /// </summary>
    public class Webhook
    {
        /// <summary>
        /// Instantiates a new instance of a <see cref="Webhook" />
        /// </summary>
        /// <param name="handler">The handler and method to execute</param>
        /// <param name="payload">The payload of the webhook to execute</param>
        public Webhook(HandlerMethod handler, ActivityPayload payload)
        {
            Handler = handler;
            Payload = payload;

        }
        /// <summary>
        /// Gets the HandlerMethod
        /// </summary>
        /// <value></value>
        public HandlerMethod Handler { get; }
        /// <summary>
        /// Gets the <see cref="ActivityPayload" /> 
        /// </summary>
        /// <value></value>
        public ActivityPayload Payload { get; }

        /// <summary>
        /// Returns a string representation of the <see cref="Webhook" />
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Webhook for '{this.Payload?.GetType().FullName ?? "NULL"}' on '{this.Handler?.Type.FullName ?? "[NULL]"}:{this.Handler?.Method.Name ?? "[NULL]"}'";
        }
    }
}