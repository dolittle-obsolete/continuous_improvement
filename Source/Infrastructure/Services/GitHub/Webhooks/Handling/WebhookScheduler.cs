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
    /// An implemention of <see cref="IWebhookScheduler" /> using a <see cref="BlockingCollection" />
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
            Task.Run(async () => await Process());
        }

        /// <inheritdoc />
        public void QueueWebhookEventForHandling(HandlerMethod handlerMethod, ActivityPayload payload)
        {
            var info = $"Webhook for '{payload?.GetType().FullName ?? "NULL"}' on '{handlerMethod?.Type.FullName ?? "[NULL]"}:{handlerMethod?.Method.Name ?? "[NULL]"}'";
            _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULING: {info}");
            _blockingCollection.Add(new Webhook(handlerMethod,payload));
            _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULED: {info}");
        }

        async Task Process()
        {
            foreach (var operation in _blockingCollection.GetConsumingEnumerable())
            {
                var info = $"Operation for '{operation?.Payload.GetType().FullName ?? "NULL"}' on '{operation?.Handler?.Type.FullName ?? "[NULL]"}:{operation?.Handler?.Method.Name ?? "[NULL]"}'";
                _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSING: {info}");
                try
                {
                    await _processor.Process(operation);
                    _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSED: {info}");
                }
                catch(Exception ex)
                {
                    _logger.Error(ex,"ERROR", info);
                }
            }
        }
    }
}