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

    public class error_throwing_number_payload_processor : ICanHandleGitHubWebhooks
    {
        public List<int> _values;
        private readonly ILogger _logger;

        public error_throwing_number_payload_processor(List<int> values, ILogger logger)
        {
            _values = values;
            _logger = logger;
        }

        public Task On(for_WebhookScheduler.given.Payload payload)
        {
            var start = DateTimeOffset.UtcNow;
            //_logger.Information($"{start.ToString()} {GetHashCode()} {nameof(error_throwing_number_payload_processor)} about started to process {payload.Number}");
            Task.Delay(10).Wait();
            if(payload.Number % 5 == 0)
            {
                //_logger.Error($"{DateTimeOffset.UtcNow.ToString()} {GetHashCode()} {nameof(error_throwing_number_payload_processor)} {payload.Number}");
                throw new ArgumentException("Cannot have a number divisible by 5");
            }
                
            _values.Add(payload.Number);
            return Task.CompletedTask;
            //var complete = DateTimeOffset.UtcNow;
            //_logger.Information($"{complete.ToString()} {GetHashCode()} ERROR finished processing {payload.Number}. Took {(complete - start).TotalMilliseconds}");
        }
    }
}