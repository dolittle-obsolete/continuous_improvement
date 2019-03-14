using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Tenancy;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    [Singleton]
    public class WebhookCoordinator : IWebhookCoordinator
    {
        readonly IInstallationToTenantMapper _tenantMapper;
        readonly FactoryFor<IWebhookScheduler> _schedulerFactory;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;
        private readonly IWebhookHandlerRegistry _handlerRegistry;

        public WebhookCoordinator(
            IInstallationToTenantMapper tenantMapper,
            FactoryFor<IWebhookScheduler> schedulerFactory,
            IExecutionContextManager executionContextManager,
            IWebhookHandlerRegistry handlerRegistry,
            ILogger logger
        )
        {
            _tenantMapper = tenantMapper;
            _schedulerFactory = schedulerFactory;
            _executionContextManager = executionContextManager;
            _logger = logger;
            _handlerRegistry = handlerRegistry;
        }

        /// <inheritdoc />
        public bool WillHandle<T>() where T : ActivityPayload
        {
            return _handlerRegistry.GetHandlersFor(typeof(T)).Any();
        }

        /// <inheritdoc />
        public void HandleWebhookPayload(ActivityPayload payload, Guid deliveryId)
        {
            IEnumerable<HandlerMethod> handlerMethods = _handlerRegistry.GetHandlersFor(payload.GetType());
            if (handlerMethods.Any())
            {
                // Figure out the execution context for this event
                var tenantId = _tenantMapper.GetTenantFor(payload.Installation.Id);

                if (tenantId == TenantId.Unknown)
                {
                    _logger.Warning($"GitHub installation '{payload.Installation.Id}' is not mapped to a tenant. The webhook will be ignored.");
                    return;
                }

                _executionContextManager.CurrentFor(tenantId, deliveryId);

                // Schedule the event for processing
                var scheduler = _schedulerFactory();
                foreach (var handler in handlerMethods)
                {
                    scheduler.QueueWebhookEventForHandling(handler, payload);
                }
            }
        }
    }
}