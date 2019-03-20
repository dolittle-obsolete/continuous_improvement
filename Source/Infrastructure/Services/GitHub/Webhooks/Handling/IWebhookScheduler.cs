/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Reflection;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// Defines a scheduler for handling incoing webhooks
    /// </summary>
    public interface IWebhookScheduler
    {
        /// <summary>
        /// Queues the webhook event for handling
        /// </summary>
        /// <param name="webhook">The incoming webhook</param>
        void QueueWebhookEventForHandling(Webhook webhook);
    }
}