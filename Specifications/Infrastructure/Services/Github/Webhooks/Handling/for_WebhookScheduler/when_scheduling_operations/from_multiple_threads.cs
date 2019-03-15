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
    public class from_multiple_threads 
    {
        static object locker = new object();
        static List<int> values;
        static List<Thread> threads;
        static List<int> myResults;
        static IWebhookScheduler scheduler;
        static Mock<IWebhookProcessor> processor;
        static int counter = 0;
        Establish context = () => 
        {
            var dependencies = given.dependencies.get();
            scheduler = dependencies.scheduler;
            processor = dependencies.processor;
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
                var operation = given.dependencies.build_webhook(new given.Payload(counter));
                scheduler.QueueWebhookEventForHandling(new Webhook(operation.Handler,operation.Payload));
                Interlocked.Increment(ref counter);
            }
        }

        It should_process_all_webhooks_in_the_order_in_which_they_are_scheduled = () => 
        {
            var expected = Enumerable.Range(0,30).ToList();
            Thread.Sleep(500);
            myResults.ShouldEqual(expected);
        };
    }
}