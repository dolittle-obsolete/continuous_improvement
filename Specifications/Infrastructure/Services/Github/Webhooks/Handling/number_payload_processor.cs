/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dolittle.Logging;
using Infrastructure.Services.Github.Webhooks.EventPayloads;

namespace Infrastructure.Services.Github.Webhooks.Handling
{

    public class number_payload_processor : ICanHandleGitHubWebhooks
    {
        public List<int> _values;
        private readonly ILogger _logger;

        public number_payload_processor(List<int> values, ILogger logger)
        {
            _values = values;
            _logger = logger;
        }

        public void On(for_WebhookScheduler.given.Payload payload)
        {
            var start = DateTimeOffset.UtcNow;
            _logger.Information($"{start.ToString()} {GetHashCode()} VOID about started to process {payload.Number}");
            Task.Delay(10).Wait();
            _values.Add(payload.Number);
            var complete = DateTimeOffset.UtcNow;
            _logger.Information($"{complete.ToString()} {GetHashCode()} VOID finished processing {payload.Number}. Took {(complete - start).TotalMilliseconds}");
        }
    }
}