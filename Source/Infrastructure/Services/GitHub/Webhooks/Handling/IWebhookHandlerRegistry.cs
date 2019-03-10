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
    /// <summary>
    /// A registry of handlers and methods that can handle ActivityPayloads
    /// </summary>
    public interface IWebhookHandlerRegistry
    {
        void RegisterHandlerMethod(Type payloadType, HandlerMethod handlerMethod);
        IEnumerable<HandlerMethod> GetHandlersFor(Type payloadType);
    }
}