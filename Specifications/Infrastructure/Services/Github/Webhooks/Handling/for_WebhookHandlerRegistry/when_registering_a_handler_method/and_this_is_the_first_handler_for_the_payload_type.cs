/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Infrastructure.Services.Github.Webhooks.Handling.for_Coordinator;
using Infrastructure.Services.Github.Webhooks.Handling;
using System.Linq;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookHandlerRegistry.when_registering_handlers
{
    [Subject(typeof(IWebhookCoordinator),"Register")]
    public class and_this_is_the_first_handler_for_the_payload_type : given.a_webhook_handler_registry
    {
        static HandlerMethod handler_method;

        Establish context = () =>  handler_method = create_from<first_handler>();

        Because of = () => registry.RegisterHandlerMethod(typeof(first_handler),handler_method);

        It should_register_the_handler_method = () => registry.GetHandlersFor(typeof(first_handler)).ShouldContainOnly(new[]{ handler_method });
    }
}