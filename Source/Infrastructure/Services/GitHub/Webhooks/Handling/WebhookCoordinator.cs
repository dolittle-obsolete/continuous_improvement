using System;
using System.Collections.Generic;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    [Singleton]
    public class WebhookCoordinator : IWebhookCoordinator
    {
        readonly ITenantMapper _tenantMapper;
        readonly FactoryFor<IWebhookScheduler> _schedulerFactory;
        readonly IExecutionContextManager _executionContextManager;

        public WebhookCoordinator(
            ITenantMapper tenantMapper,
            FactoryFor<IWebhookScheduler> schedulerFactory,
            IExecutionContextManager executionContextManager
        )
        {
            _tenantMapper = tenantMapper;
            _schedulerFactory = schedulerFactory;
            _executionContextManager = executionContextManager;
        }

        class Handler {
            public Type Type { get; set; }
            public MethodInfo Method { get; set; }
        }

        Dictionary<Type,List<Handler>> _registeredHandlers = new Dictionary<Type, List<Handler>>();

        /// <inheritdoc />
        public void RegisterHandlerMethod(Type payloadType, Type handler, MethodInfo method)
        {
            var handlerEntry = new Handler{ Type = handler, Method = method };
            if (_registeredHandlers.TryGetValue(payloadType, out var handlers))
            {
                handlers.Add(handlerEntry);
            }
            else
            {
                _registeredHandlers.Add(payloadType, new List<Handler>{ handlerEntry });
            }
        }

        /// <inheritdoc />
        public bool WillHandle<T>() where T : ActivityPayload
        {
            return _registeredHandlers.ContainsKey(typeof(T));
        }

        /// <inheritdoc />
        public void HandleWebhookPayload(ActivityPayload payload, Guid deliveryId)
        {
            if (_registeredHandlers.TryGetValue(payload.GetType(), out var handlers))
            {
                // Figure out the execution context for this event
                var tenantId = _tenantMapper.GetTenantFor(payload.Installation);
                _executionContextManager.CurrentFor(tenantId, deliveryId);

                // Schedule the event for processing
                var scheduler = _schedulerFactory();
                foreach (var handler in handlers)
                {
                    scheduler.QueueWebhookEventForHandling(handler.Type, handler.Method, payload);
                }
            }
        }
    }
}