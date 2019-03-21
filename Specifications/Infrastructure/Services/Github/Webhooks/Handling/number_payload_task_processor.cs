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

    public class number_payload_task_processor : ICanHandleGitHubWebhooks
    {
        public List<int> _values;

        public number_payload_task_processor(List<int> values)
        {
            _values = values;
        }

        public async Task On(for_WebhookScheduler.given.Payload payload)
        {
            var start = DateTimeOffset.UtcNow;
            await Task.Delay(10);
            _values.Add(payload.Number);
            var complete = DateTimeOffset.UtcNow;
        }
    }
}