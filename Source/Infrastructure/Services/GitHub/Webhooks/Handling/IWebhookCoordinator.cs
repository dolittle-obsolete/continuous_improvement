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
    /// Defines a coordinator for processing a webhook request
    /// </summary>
    public interface IWebhookCoordinator
    {
        /// <summary>
        /// Indicates whether the coordinator can handle the incoming <see cref="ActivityPayload" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool WillHandle<T>() where T : ActivityPayload;

        /// <summary>
        /// Handles the incoming <see cref="ActivityPayload" /> with a correlation (delivery) id
        /// </summary>
        /// <param name="payload">The incoming payload</param>
        /// <param name="deliveryId">A correlation id</param>
        void HandleWebhookPayload(ActivityPayload payload, Guid deliveryId);
    }
}