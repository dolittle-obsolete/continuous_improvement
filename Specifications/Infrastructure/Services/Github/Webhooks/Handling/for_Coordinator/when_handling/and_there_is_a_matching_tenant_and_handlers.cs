/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Tenancy;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Octokit;
using It = Machine.Specifications.It;
using Moq;
using Dolittle.Collections;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Coordinator.when_handling
{
    [Subject(typeof(IWebhookCoordinator),"HandleWebhookPayload")]
    public class and_there_is_a_matching_tenant_and_handlers : given.a_webhook_coordinator
    {
        static long installation_id;
        static ActivityPayload payload_event;
        static TenantId tenant;
        static IEnumerable<HandlerMethod> handler_methods;
        Establish context = () =>
        {
            tenant = Guid.NewGuid();
            installation_id = 1;
            payload_event = new ActivityPayload(null,null,new InstallationId(installation_id));
            handler_methods = HandlerMethod.GetUsableHandlerMethodsFrom(typeof(first_handler));
            registry.Setup(_ => _.GetHandlersFor(typeof(ActivityPayload))).Returns(handler_methods);
            installation_tenant_mapper.Setup(_ => _.GetTenantFor(installation_id)).Returns(tenant);
        };

        Because of = () => coordinator.HandleWebhookPayload(payload_event,delivery_id);

        It should_set_the_correct_tenant_scope = () => execution_context_manager.Verify(_ => _.CurrentFor(tenant,delivery_id,Moq.It.IsAny<string>(),Moq.It.IsAny<int>(),Moq.It.IsAny<string>()));
        It should_schedule_each_registered_handler_method = () => 
        {
            handler_methods.ForEach(_ => scheduler.Verify(s => s.QueueWebhookEventForHandling(_,payload_event),Times.Once()));
        };
     }
}