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
    public class and_there_are_no_handlers_registered : given.a_webhook_handler_registry
    {
        static IEnumerable<HandlerMethod> registered_methods;

        Because of = () => registered_methods = registry.GetHandlersFor(typeof(first_handler));

        It should_return_no_handlers = () => registered_methods.ShouldBeEmpty();
    }
}