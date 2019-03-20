/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System.Linq;
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;
using Moq;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookHandlerRegistry.given
{
    public class a_webhook_handler_registry
    {
        protected static IWebhookHandlerRegistry registry;

        Establish context = () => registry = new WebhookHandlerRegistry();

        protected static HandlerMethod create_from<T>()
        {
            return new HandlerMethod(typeof(T), typeof(T).GetMethods().First());
        }
    }
}