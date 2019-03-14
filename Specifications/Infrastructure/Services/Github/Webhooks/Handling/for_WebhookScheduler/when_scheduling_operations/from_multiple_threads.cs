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

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.when_scheduling_operations
{
    public class from_multiple_threads : given.a_webhook_scheduler_handler
    {
        static object locker = new object();
        static List<int> values;
        static List<Thread> threads;
        static List<int> results;

        static int counter = 0;
        Establish context = () => 
        {
            values = Enumerable.Range(0,10).ToList();
            results = new List<int>();
            threads = new List<Thread>();

            processor.Setup(_ => _.Process(Moq.It.IsAny<Webhook>()))
                .Returns(async(Webhook _) => {
                    await Task.Delay(10);
                    var payload = (given.Payload)_.Payload;
                    results.Add(payload.Number);
                    await Task.FromResult(payload.Number);
                });
        };

        Because of = () => 
        {
            Thread first = new Thread(new ThreadStart(() => values.ForEach(_ => Schedule())));
            Thread second = new Thread(new ThreadStart(() => values.ForEach(_ => Schedule())));
            Thread third = new Thread(new ThreadStart(() => values.ForEach(_ => Schedule())));
            threads.Add(first);
            threads.Add(second);
            threads.Add(third);
            first.Start();
            second.Start();
            third.Start();
            while(threads.Any(_ => _.IsAlive))
                Thread.Sleep(10);
        };

        static void Schedule()
        {
            lock(locker)
            {
                var operation = BuildWebhookOperation(new given.Payload(counter));
                scheduler.QueueWebhookEventForHandling(operation.Handler,operation.Payload);
                Interlocked.Increment(ref counter);
            }
        }

        It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = () => 
        {
            Thread.Sleep(1000);
            results.ShouldEqual(Enumerable.Range(0,30).ToList());
        };
    }
}