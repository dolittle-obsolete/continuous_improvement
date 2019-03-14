/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// Encapsulates how to call a handler method for a webhook
    /// </summary>
    public interface IWebhookProcessor
    {
        /// <summary>
        /// Processes the <see cref="ActivityPayload" /> with this handler and method
        /// </summary>
        /// <param name="webhook">Webhook to process</param>
        /// <returns>A Task</returns>
        Task Process(Webhook webhook);
    }

    /// <summary>
    /// An implemenation of <see cref="IWebhookProcessor" /> 
    /// </summary>
    public class WebhookProcessor : IWebhookProcessor
    {
        private readonly IContainer _container;

        /// <summary>
        /// Instantiates an instance of <see cref="WebhookProcessor" /> 
        /// </summary>
        /// <param name="container">A container to instantiate instances of the handler</param>
        public WebhookProcessor(IContainer container)
        {
            _container = container;
        }

        /// <inheritdoc />
        public Task Process(Webhook webhook)
        {
            var parameters = new object[] { webhook.Payload };
            var instance = _container.Get(webhook.Handler.Type);
            var method = webhook.Handler.Method;
            if(IsVoidMethod(method))
                return ProcessVoidMethod(instance,method,parameters);

            return ProcessTaskMethod(instance,method,parameters);
            
        }

        Task ProcessVoidMethod(object instance, MethodInfo method, object[] parameters) 
        {
            return Task.Run(() => method.Invoke(instance,parameters));
        }

        Task ProcessTaskMethod(object instance, MethodInfo method, object[] parameters)
        {
            return (Task)method.Invoke(instance,parameters);
        }

        bool IsVoidMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(void);
        }
    }
}