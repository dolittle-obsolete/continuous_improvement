/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Lifecycle;

namespace Infrastructure.Services.Github.Webhooks.Handling
{

    [SingletonPerTenant]
    public class WebhookHandlerRegistry : IWebhookHandlerRegistry
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<HandlerMethod>> _registeredHandlers;

        public WebhookHandlerRegistry()
        {
            _registeredHandlers = new ConcurrentDictionary<Type, ConcurrentBag<HandlerMethod>>();
        }

        public IEnumerable<HandlerMethod> GetHandlersFor(Type payloadType)
        {
            ConcurrentBag<HandlerMethod> handlers;
            return _registeredHandlers.TryGetValue(payloadType, out handlers) ? handlers.ToArray() : Enumerable.Empty<HandlerMethod>();
        }

        public void RegisterHandlerMethod(Type payloadType, HandlerMethod handlerMethod)
        {
            _registeredHandlers.AddOrUpdate(payloadType, new ConcurrentBag<HandlerMethod>{ handlerMethod }, (key, list) => AppendToExisting(list,handlerMethod));
        }

        ConcurrentBag<HandlerMethod> AppendToExisting(ConcurrentBag<HandlerMethod> list, HandlerMethod toAdd)
        {
            list.Add(toAdd);
            return list;
        }
    }
}