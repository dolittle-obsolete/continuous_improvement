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
    /// An implemention of <see cref="IWebhookScheduler" />
    /// </summary>
    [SingletonPerTenant]
    public class WebhookScheduler : IWebhookScheduler
    {
        private Queue<Webhook> _queue;
        private object locker = new object();
        private readonly IWebhookProcessor _processor;
        private readonly ILogger _logger;
        private bool _backgroundProcessingRunning = false;

        /// <summary>
        /// Instantiates a new instance of <see cref="WebhookScheduler" />
        /// </summary>
        /// <param name="processor">A processor that can process the queued webhook</param>
        /// <param name="logger">A logger to log information</param>
        public WebhookScheduler(IWebhookProcessor processor, ILogger logger)
        {
            _queue = new Queue<Webhook>();
            _processor = processor;
            _logger = logger;
        }

        /// <inheritdoc />
        public void QueueWebhookEventForHandling(Webhook webhook)
        {
            lock(locker)
            {
                _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULING: {webhook}");
                _queue.Enqueue(webhook);
                ScheduleProcessQueueTask();
                _logger.Information($"{DateTime.UtcNow.ToString()} SCHEDULED: {webhook}");
            }
        }

        void Process(object obj)
        {
            while(true)
            {
                Webhook webhook;
                lock(locker)
                {
                    if(_queue.Count == 0)
                    {
                        _backgroundProcessingRunning = false;
                        break;
                    }
                    webhook = _queue.Dequeue();
                }
                try
                {
                    _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSING: {webhook}");
                    _processor.Process(webhook).Wait();
                    _logger.Information($"{DateTime.UtcNow.ToString()} PROCESSED: {webhook}");
                }
                catch(Exception ex)
                {
                    var errorMsg = $"Processing of queued github webhook task failed: {webhook?.Handler?.Type.FullName ?? "[NULL]"} {webhook?.Handler?.Method.Name ?? "[NULL]"} {webhook?.Payload?.GetType().FullName ?? "[NULL]"}";
                    _logger.Error(new GitHubWebHookProcessingFailure(webhook?.ToString() ?? "[NULL]",ex),errorMsg);
                }
            }
        }

        void ScheduleProcessQueueTask()
        {
            if (!_backgroundProcessingRunning)
            {
                _backgroundProcessingRunning = true;
                ThreadPool.UnsafeQueueUserWorkItem(Process, null);
            }
        }
    }
}