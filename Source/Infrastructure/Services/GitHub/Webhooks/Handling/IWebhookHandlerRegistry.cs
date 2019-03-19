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
        /// <summary>
        /// Registers a <see cref="HandlerMethod" /> for a particular payload <see cref="Type" />
        /// </summary>
        /// <param name="payloadType">The type of the payload</param>
        /// <param name="handlerMethod">A <see cref="HandlerMethod" /> for this type</param>
        void RegisterHandlerMethod(Type payloadType, HandlerMethod handlerMethod);

        /// <summary>
        /// Returns all the <see cref="HandlerMethod">handler methods</see> for a <see cref="Type">payload type</see>
        /// </summary>
        /// <param name="payloadType">The <see cref="Type" /> of the activity handler</param>
        /// <returns></returns>
        IEnumerable<HandlerMethod> GetHandlersFor(Type payloadType);
    }
}