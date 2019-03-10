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
using System.Collections.Generic;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookHandlerRegistry.when_getting_handler_methods
{
    [Subject(typeof(IWebhookCoordinator),"Register")]
    public class and_there_is_a_single_handler_registered : given.a_webhook_handler_registry
    {
        static HandlerMethod handler;
        static IEnumerable<HandlerMethod> registered_methods;

        Establish context = () =>  
        {
            handler = create_from<second_handler>();
            registry.RegisterHandlerMethod(typeof(first_handler),handler);
        };

        Because of = () => registered_methods = registry.GetHandlersFor(typeof(first_handler));

        It should_return_the_registered_handler = () => registered_methods.ShouldContainOnly(new[]{ handler });
    }
}