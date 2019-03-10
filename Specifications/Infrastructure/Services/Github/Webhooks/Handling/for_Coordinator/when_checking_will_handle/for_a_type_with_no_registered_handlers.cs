/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Linq;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Coordinator.when_checking_will_handle
{
    [Subject(typeof(IWebhookCoordinator),"WillHandle")]
    public class for_a_type_with_no_registered_handlers : given.a_webhook_coordinator
    {
        static bool will_handle;
        Establish context = () => 
        {
            registry.Setup(_ => _.GetHandlersFor(typeof(CreateEventPayload))).Returns(Enumerable.Empty<HandlerMethod>());
        };

        Because of = () => will_handle = coordinator.WillHandle<CreateEventPayload>();

        It should_not_handle = () => will_handle.ShouldBeFalse();
    }
}