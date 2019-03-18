/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using System.Threading;
using Core.SourceControl.GitHub;
using Dolittle.Execution;
using Dolittle.Security;
using Dolittle.Tenancy;
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;
using Moq;
using ExecutionContext = Dolittle.Execution.ExecutionContext;

namespace Core.Specs.SourceControl.GitHub.for_InstallationEventProcessor.given
{
    public class an_installation_event_processor_for<T>
    {
        protected static InstallationEventProcessor event_processor;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        protected static Mock<IInstallationToTenantMapper> installation_to_tenant_mapper;
        protected static ExecutionContext execution_context;
        protected static TenantId tenant;
        protected static int installation_id;
        Establish context = () =>
        {
            installation_id = 100;
            tenant = Guid.NewGuid();
            execution_context = new ExecutionContext(Guid.NewGuid(), Guid.NewGuid(), tenant,
                Dolittle.Execution.Environment.Development, Guid.NewGuid(),
                Claims.Empty, Thread.CurrentThread.CurrentCulture);
            execution_context_manager = new Mock<IExecutionContextManager>();
            execution_context_manager.SetupGet(_ => _.Current).Returns(execution_context);
            installation_to_tenant_mapper = new Mock<IInstallationToTenantMapper>();

            event_processor = new InstallationEventProcessor(execution_context_manager.Object, installation_to_tenant_mapper.Object);
        };
    }
}