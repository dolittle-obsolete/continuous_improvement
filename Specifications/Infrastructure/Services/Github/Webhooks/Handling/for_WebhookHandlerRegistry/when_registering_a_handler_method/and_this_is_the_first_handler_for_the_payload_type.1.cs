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
    public class and_this_is_the_second_handler_for_the_payload_type : given.a_webhook_handler_registry
    {
        static HandlerMethod already_registered;
        static HandlerMethod handler_method;

        Establish context = () =>  
        {
            already_registered = create_from<second_handler>();
            handler_method = create_from<first_handler>();
            registry.RegisterHandlerMethod(typeof(first_handler),handler_method);
        };

        Because of = () => registry.RegisterHandlerMethod(typeof(first_handler),already_registered);

        It should_register_the_handler_method_alongside_the_existing_handler = () => registry.GetHandlersFor(typeof(first_handler)).ShouldContainOnly(new[]{ already_registered, handler_method });
    }
}