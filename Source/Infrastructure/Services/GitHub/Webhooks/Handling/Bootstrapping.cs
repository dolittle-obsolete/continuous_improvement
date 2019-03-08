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
        readonly IWebhookCoordinator _coordinator;

        public Bootstrapping(
            ILogger logger,
            IImplementationsOf<ICanHandleGitHubWebhooks> handlers,
            IWebhookCoordinator coordinator
        )
        {
            _logger = logger;
            _handlers = handlers;
            _coordinator = coordinator;
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
                var handlerMethods = GetUsableHandlerMethods(handler);
            
                if(!handlerMethods.Any())
                    continue;

                _logger.Information($"GitHubWebHookHandlers - Type {handler.FullName} can handle webhooks");

                handlerMethods.ForEach(_ => RegisterHandlerMethod(_,handler));
            }
        }

        IEnumerable<MethodInfo> GetUsableHandlerMethods(Type handler)
        {
            return handler.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     .Where(_ => IsAWebHookMethod(_));
        }

        bool IsAWebHookMethod(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType == typeof(void) && methodInfo.Name == "On" && methodInfo.GetParameters().Length == 1 && methodInfo.GetParameters().All(p => typeof(ActivityPayload).IsAssignableFrom(p.ParameterType));
        }

        void RegisterHandlerMethod(MethodInfo method, Type handler)
        {
            var eventType = method.GetParameters().First().ParameterType;
            _coordinator.RegisterHandlerMethod(eventType, handler, method);
            _logger.Information($"GitHubWebHookHandlers - Registered {handler.FullName} for handling event {eventType.Name}");
        }
    }
}