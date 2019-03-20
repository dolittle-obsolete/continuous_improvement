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
    public class and_there_are_multiple_handlers_registered : given.a_webhook_handler_registry
    {
        static HandlerMethod first;
        static HandlerMethod second;
        static IEnumerable<HandlerMethod> registered_methods;

        Establish context = () =>  
        {
            first = create_from<second_handler>();
            second = create_from<first_handler>();
            registry.RegisterHandlerMethod(typeof(first_handler),first);
            registry.RegisterHandlerMethod(typeof(first_handler),second);
        };

        Because of = () => registered_methods = registry.GetHandlersFor(typeof(first_handler));

        It should_return_the_registered_handlers = () => registered_methods.ShouldContainOnly(new[]{ first, second });
    }
}