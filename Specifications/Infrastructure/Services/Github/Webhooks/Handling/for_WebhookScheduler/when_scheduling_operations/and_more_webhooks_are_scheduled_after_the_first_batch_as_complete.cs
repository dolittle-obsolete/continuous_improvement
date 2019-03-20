/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.when_scheduling_operations
{
    [Subject(typeof(IWebhookScheduler), "QueueWebhookEventForHandling")]
    public class and_more_webhooks_are_scheduled_after_the_first_batch_as_complete
        : given.a_webhook_scheduler_for<and_more_webhooks_are_scheduled_after_the_first_batch_as_complete>
        {
            static List<int> values;
            static List<given.Payload> payloads;
            static List<int> results;

            Establish context = () =>
            {
                values = Enumerable.Range(0, 10).ToList();
                results = new List<int>();
                payloads = new List<given.Payload>();

                values.ForEach(v => payloads.Add(new given.Payload(v)));

                processor.Setup(_ => _.Process(Moq.It.IsAny<Webhook>()))
                    .Returns(async(Webhook _) =>
                    {
                        await Task.Delay(10).ConfigureAwait(false);
                        var payload = (given.Payload)_.Payload;
                        results.Add(payload.Number);
                    });
            };

            Because of = async() =>
            {
                payloads.ForEach(_ =>
                {
                    var webhook = given.a.webhook_from(_);
                    scheduler.QueueWebhookEventForHandling(webhook);
                });
                await Task.Delay(1000);
                payloads.ForEach(_ =>
                {
                    var webhook = given.a.webhook_from(_);
                    scheduler.QueueWebhookEventForHandling(webhook);
                });
            };

            It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = async() =>
            {
                await Task.Delay(200).ConfigureAwait(false);
                results.ShouldEqual(values);
            };
        }
}