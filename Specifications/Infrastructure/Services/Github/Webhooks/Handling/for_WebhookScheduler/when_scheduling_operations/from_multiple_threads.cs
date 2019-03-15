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
    [Subject(typeof(IWebhookScheduler),"QueueWebhookEventForHandling")]
    public class from_multiple_threads : given.a_webhook_scheduler_for<from_multiple_threads>
    {
        static object locker = new object();
        static List<int> values;
        static List<Thread> threads;
        static List<int> myResults;
        static int counter = 0;
        Establish context = () => 
        {
            values = Enumerable.Range(0,10).ToList();
            myResults = new List<int>();
            threads = new List<Thread>();

            processor.Setup(_ => _.Process(Moq.It.IsAny<Webhook>()))
                .Returns(async(Webhook _) => {
                    await Task.Delay(10);
                    var payload = (given.Payload)_.Payload;
                    myResults.Add(payload.Number);
                });
        };

        Because of = () => 
        {
            create_scheduling_thread();
            create_scheduling_thread();
            create_scheduling_thread();
            threads.ForEach(_ => _.Start());
            while(threads.Any(_ => _.IsAlive))
                Thread.Sleep(10);
        };

        static void create_scheduling_thread()
        {
            var thread = new Thread(new ThreadStart(() => values.ForEach(_ => schedule())));
            threads.Add(thread);
        }

        static void schedule()
        {
            lock(locker)
            {
                var operation = given.a.webhook_from(new given.Payload(counter));
                scheduler.QueueWebhookEventForHandling(new Webhook(operation.Handler,operation.Payload));
                Interlocked.Increment(ref counter);
            }
        }

        It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = () => 
        {
            var expected = Enumerable.Range(0,30).ToList();
            Thread.Sleep(500);
            myResults.ShouldEqual(null);
        };
    }
}