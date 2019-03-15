/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Moq;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.given
{
    public class dependencies
    {
        private dependencies(IWebhookScheduler s, Mock<IWebhookProcessor> p)
        {
            scheduler = s;
            processor = p;
        }
        public IWebhookScheduler scheduler { get; }
        public Mock<IWebhookProcessor> processor { get; }

        public static dependencies get()
        {
            var processor = new Mock<IWebhookProcessor>();
            var scheduler = new WebhookScheduler(processor.Object, new ConsoleLogger());
            return new dependencies(scheduler, processor);
        }

        public static Webhook build_webhook(Payload payload)
        {
            var type = typeof(first_handler);
            return new Webhook(new HandlerMethod(type, type.GetMethods().First()), payload);
        }
    }

    public class Payload : ActivityPayload
    {
        public Payload(int value) => Number = value;
        public int Number { get; }
    }
}