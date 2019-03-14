/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    [SingletonPerTenant]
    public class WebhookScheduler : IWebhookScheduler
    {        
        private readonly ILogger _logger;
        IContainer _container;
        TaskQueue _taskQueue;

        //
        public WebhookScheduler(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
            _taskQueue = new TaskQueue();
        }

        public void QueueWebhookEventForHandling(HandlerMethod handlerMethod, ActivityPayload payload)
        {
            Console.WriteLine("Queueing");
            Action<Task> errorLogging = (t) => _logger.Error(t.Exception, $"Error when handling '{payload?.GetType().FullName ?? "[NULL]"}' on '{handlerMethod?.Type?.FullName ?? "[NULL]"} : {handlerMethod?.Method?.Name ?? "[NULL]"}'");
            _taskQueue.Enqueue(async () => {
                if (handlerMethod.Method.ReturnType == typeof(Task))
                {
                    Console.WriteLine("Awaiting a task");
                    var task = (Task)handlerMethod.Method.Invoke(_container.Get(handlerMethod.Type), new object[] { payload } );
                    await task;
                }
                else
                {
                    Console.WriteLine("Not a task");
                    handlerMethod.Method.Invoke(_container.Get(handlerMethod.Type), new object[] { payload } );
                }

            }, errorLogging);
        }     
    }

    // public class WebHookHandlerTaskQueue
    // {
    //     TaskQueue _taskQueue;
    //     public WebHookHandlerTaskQueue()
    //     {
    //         _taskQueue = new TaskQueue();
    //     }

    //     public Enqueue(object instance, MethodInfo method, ActivityPayload payload)
    //     {
            
    //     }
    // }
}