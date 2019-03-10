/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using System;
using System.Reflection;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Infrastructure.Services.Github.Webhooks.Handling.for_Bootstrapping.given;
using Moq;
using It = Machine.Specifications.It;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Bootstrapping.when_performing
{
    public class with_discovered_handlers_with_on_methods : given.a_bootstrapper
    {
        Because of = () => bootstrapper.Perform();

        It should_register_a_method_for_the_create_event_payload = 
            () => webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(typeof(CreateEventPayload),IsHandlerMethodForType<first_handler>()),Times.Once());
        It should_register_two_methods_for_the_delete_event_payload = 
            () => {
                webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(typeof(DeleteEventPayload),IsHandlerMethodForType<first_handler>()),Times.Once());
                webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(typeof(DeleteEventPayload),IsHandlerMethodForType<second_handler>()),Times.Once());
            };
        It should_register_a_method_for_the_installation_event_payload = 
            () => webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(typeof(InstallationEventPayload),IsHandlerMethodForType<second_handler>()),Times.Once());
        It should_register_a_method_for_the_installation_repositories_event_payload = 
            () => webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(typeof(InstallationRepositoriesEventPayload),IsHandlerMethodForType<second_handler>()),Times.Once()); 
        It should_not_register_any_methods_for_the_implementation_with_no_methods = 
            () => webhook_handler_registry.Verify(_ => _.RegisterHandlerMethod(Moq.It.IsAny<Type>(),
                                                                            IsHandlerMethodForType<handler_with_no_implementations>())
                                                                            ,Times.Never());       
    }
}