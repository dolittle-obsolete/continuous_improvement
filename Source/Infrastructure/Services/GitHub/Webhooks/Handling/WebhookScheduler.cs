/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// An implemention of <see cref="IWebhookScheduler" /> using a <see cref="BlockingCollection{Webhook}" />
    /// </summary>
    [SingletonPerTenant]
    public class WebhookScheduler : IWebhookScheduler
    {
        private readonly BlockingCollection<Webhook> _blockingCollection;
        private readonly IWebhookProcessor _processor;
        private readonly ILogger _logger;

        /// <summary>
        /// Instantiates a new instance of <see cref="WebhookScheduler" />
        /// </summary>
        /// <param name="processor">A processor that can process the queued webhook</param>
        /// <param name="logger">A logger to log information</param>
        public WebhookScheduler(IWebhookProcessor processor, ILogger logger)
        {
            _blockingCollection = new BlockingCollection<Webhook>();
            _processor = processor;
            _logger = logger;
            Task.Run(async () => await Process().ConfigureAwait(false));
        }

        /// <inheritdoc />
        public void QueueWebhookEventForHandling(Webhook webhook)
        {
            _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULING: {webhook}");
            _blockingCollection.Add(webhook);
            _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULED: {webhook}");
        }

        async Task Process()
        {
            foreach (var webhook in _blockingCollection.GetConsumingEnumerable())
            {
                _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSING: {webhook}");
                try
                {
                    await _processor.Process(webhook).ConfigureAwait(false);
                    _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSED: {webhook}");
                }
                catch(Exception ex)
                {
                    _logger.Error(ex,"ERROR", webhook.ToString());
                }
            }
        }
    }
}