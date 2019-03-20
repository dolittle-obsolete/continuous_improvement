/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Execution;
using Dolittle.Logging;
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;
using Moq;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Coordinator.given
{
    public class a_webhook_coordinator
    {
        protected static IWebhookCoordinator coordinator;

        protected static Mock<IInstallationToTenantMapper> installation_tenant_mapper;
        protected static Mock<IWebhookScheduler> scheduler;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        protected static Mock<IWebhookHandlerRegistry> registry;
        protected static Mock<ILogger> logger;
        protected static Guid delivery_id;

        Establish context = () =>
        {
            delivery_id = Guid.NewGuid();
            installation_tenant_mapper = new Mock<IInstallationToTenantMapper>();
            scheduler = new Mock<IWebhookScheduler>();
            execution_context_manager = new Mock<IExecutionContextManager>();
            registry = new Mock<IWebhookHandlerRegistry>();
            logger = new Mock<ILogger>();

            coordinator = new WebhookCoordinator(installation_tenant_mapper.Object,
                () => scheduler.Object,
                execution_context_manager.Object,
                registry.Object,
                logger.Object);
        };
    }
}