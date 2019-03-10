using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Types;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public class Bootstrapping : ICanPerformBootProcedure
    {
        readonly ILogger _logger;
        readonly IImplementationsOf<ICanHandleGitHubWebhooks> _handlers;
        readonly IWebhookHandlerRegistry _registry;

        public Bootstrapping(
            ILogger logger,
            IImplementationsOf<ICanHandleGitHubWebhooks> handlers,
            IWebhookHandlerRegistry registry
        )
        {
            _logger = logger;
            _handlers = handlers;
            _registry = registry;
        }

        /// <inheritdoc/>
        public bool CanPerform()
        {
            return true;
        }
        
        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Information($"GitHubWebHookHandlers - Discovering handlers");
            
            foreach (var handler in _handlers)
            {
                var handlerMethods = HandlerMethod.GetUsableHandlerMethodsFrom(handler);
            
                if(!handlerMethods.Any())
                    continue;

                _logger.Information($"GitHubWebHookHandlers - Type {handler.FullName} can handle webhooks");

                handlerMethods.ForEach(_ => RegisterHandlerMethod(_,handler));
            }
        }

        void RegisterHandlerMethod(MethodInfo method, Type handler)
        {
            var eventType = method.GetParameters().First().ParameterType;
            _registry.RegisterHandlerMethod(eventType, new HandlerMethod(handler, method));
            _logger.Information($"GitHubWebHookHandlers - Registered {handler.FullName} for handling event {eventType.Name}");
        }
    }
}