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
    public class a_webhook_scheduler_for<T>
    {
        protected static IWebhookScheduler scheduler;
        protected static Mock<IWebhookProcessor> processor;

        Establish context = () =>
        {
            processor = new Mock<IWebhookProcessor>();
            scheduler = new WebhookScheduler(processor.Object, new ConsoleLogger());
        };

        public static Webhook webhook_from(Payload payload)
        {
            var type = typeof(first_handler);
            return new Webhook(new HandlerMethod(type, type.GetMethods().First()), payload);
        }
    }
}