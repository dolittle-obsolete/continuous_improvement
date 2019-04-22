/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.when_scheduling_operations
{
    [Subject(typeof(IWebhookScheduler), "QueueWebhookEventForHandling")]
    public class and_there_are_multiple_successful_and_failed_operations
        : given.a_webhook_scheduler_for<and_there_are_multiple_successful_and_failed_operations>
        {
            static List<int> values;
            static List<given.Payload> payloads;
            static List<int> results;
            static List<int> expected;
            static List<Task> tasks;

            Establish context = () =>
            {
                values = Enumerable.Range(0, 10).ToList();
                results = new List<int>();
                payloads = new List<given.Payload>();
                tasks = new List<Task>();
                expected = values.Where(v => v % 5 != 0).ToList();

                values.ForEach(v => payloads.Add(new given.Payload(v)));

                processor.Setup(_ => _.Process(Moq.It.IsAny<Webhook>()))
                    .Returns((Webhook _) =>
                    {
                        var payload = ((given.Payload)_.Payload);
                        var processor = new error_throwing_number_payload_processor(results,console_logger);
                        var task = processor.On(payload);
                        tasks.Add(task);
                        return task;
                    });
            };

            Because of = () =>
            {
                payloads.ForEach(_ =>
                {
                    var webhook = given.a.webhook_from(_);
                    scheduler.QueueWebhookEventForHandling(webhook);
                });
                ensure_all_tasks_are_completed(tasks, expected.Count());
            };

            It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = () =>
            {
                results.ShouldEqual(expected);
            };

            It should_log_the_errors_and_carry_on_processing = () =>
            {
                var messages = console_logger.GetLoggedMessages().ToList();
                messages.Where(_ => _.Contains("Processing of queued github webhook task failed")).Count().ShouldEqual(2);
            };
        }
}