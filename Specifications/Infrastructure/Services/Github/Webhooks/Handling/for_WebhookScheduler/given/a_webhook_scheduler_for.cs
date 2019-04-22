/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Logging;
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
        protected static ConsoleLogger console_logger;

        Establish context = () =>
        {
            console_logger = new ConsoleLogger();
            console_logger.Enable(LogLevel.Error);
            processor = new Mock<IWebhookProcessor>();
            scheduler = new WebhookScheduler(processor.Object, console_logger);
        };

        public static Webhook webhook_from(Payload payload)
        {
            var type = typeof(number_payload_processor);
            return new Webhook(HandlerMethod.GetUsableHandlerMethodsFrom(type).First(), payload);
        }

        public static Webhook webhook_with_task_from(Payload payload)
        {
            var type = typeof(number_payload_task_processor);
            return new Webhook(HandlerMethod.GetUsableHandlerMethodsFrom(type).First(), payload);
        }

        public static void ensure_all_tasks_are_completed(List<Task> tasks, int expectedNumberOfTasks)
        {
            while(tasks.Count() < expectedNumberOfTasks)
                Task.Delay(10).Wait();
                    
            try
            {
                Task.WaitAll(tasks.ToArray());
            } 
            catch
            {
                //don't let the exception bubble up
            }
        }
    }
}