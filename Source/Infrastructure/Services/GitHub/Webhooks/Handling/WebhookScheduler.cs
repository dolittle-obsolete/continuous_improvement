using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    [SingletonPerTenant]
    public class WebhookScheduler : IWebhookScheduler
    {
        IContainer _container;

        public WebhookScheduler(IContainer container)
        {
            _container = container;
        }

        readonly object _locker = new object();
        readonly Dictionary<Type, Task> _queues = new Dictionary<Type, Task>();

        public void QueueWebhookEventForHandling(HandlerMethod handlerMethod, ActivityPayload payload)
        {
            lock (_locker)
            {
                Task previousTask;
                if (!_queues.TryGetValue(handlerMethod.Type, out previousTask))
                {
                    previousTask = Task.CompletedTask;
                }
                _queues[handlerMethod.Type] = Task.Run(async () => {
                    await previousTask;
                    handlerMethod.Method.Invoke(_container.Get(handlerMethod.Type), new object[] { payload } );
                });
            }
        }
    }
}