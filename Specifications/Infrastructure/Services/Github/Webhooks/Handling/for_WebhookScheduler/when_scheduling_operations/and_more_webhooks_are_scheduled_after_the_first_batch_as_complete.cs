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
            static List<int> expected;
            static List<given.Payload> payloads;
            static List<int> results;
            static List<Task> tasks;

            Establish context = () =>
            {
                values = Enumerable.Range(0, 10).ToList();
                expected = new List<int>();
                results = new List<int>();
                tasks = new List<Task>();
                payloads = new List<given.Payload>();

                values.ForEach(v => payloads.Add(new given.Payload(v)));

                processor.Setup(_ => _.Process(Moq.It.IsAny<Webhook>()))
                    .Returns((Webhook _) =>
                    {
                        var payload = ((given.Payload)_.Payload);
                        var processor = new number_payload_task_processor(results);
                        var task = processor.On(payload);
                        tasks.Add(task);
                        return task;
                    });
                expected.AddRange(values);
                expected.AddRange(values);
            };

            Because of = () =>
            {
                payloads.ForEach(_ =>
                {
                    var webhook = given.a.webhook_from(_);
                    scheduler.QueueWebhookEventForHandling(webhook);
                });
                Task.Delay(500).Wait();
                payloads.ForEach(_ =>
                {
                    var webhook = given.a.webhook_from(_);
                    scheduler.QueueWebhookEventForHandling(webhook);
                });
                ensure_all_tasks_are_completed(tasks,expected.Count());
            };

            It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = () =>
            {
                results.ShouldEqual(expected);
            };
    }
}