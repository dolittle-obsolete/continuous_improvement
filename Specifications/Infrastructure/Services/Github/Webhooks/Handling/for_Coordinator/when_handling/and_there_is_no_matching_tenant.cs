/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Linq;
using Dolittle.Tenancy;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Coordinator.when_handling
{
    [Subject(typeof(IWebhookCoordinator),"HandleWebhookPayload")]
    public class and_there_is_no_matching_tenant : given.a_webhook_coordinator
    {
        static long installation_id;
        static ActivityPayload payload_event;
        Establish context = () =>
        {
            installation_id = 1;
            payload_event = new ActivityPayload(null,null,new InstallationId(installation_id));

            registry.Setup(_ => _.GetHandlersFor(typeof(ActivityPayload))).Returns(HandlerMethod.GetUsableHandlerMethodsFrom(typeof(first_handler)));
            installation_tenant_mapper.Setup(_ => _.GetTenantFor(installation_id)).Returns(TenantId.Unknown);
        };

        Because of = () => coordinator.HandleWebhookPayload(payload_event,delivery_id);

        It should_not_schedule_any_handlers = () => scheduler.Verify(_ => _.QueueWebhookEventForHandling(Moq.It.IsAny<Webhook>()),Moq.Times.Never());
    }
}