using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Dolittle.Booting;
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
                // Get usable handler methods
                var methods = handler.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     .Where(_ => _.ReturnType == typeof(void) && _.Name == "On" && _.GetParameters().Length == 1 && _.GetParameters().All(p => typeof(ActivityPayload).IsAssignableFrom(p.ParameterType)));
            
                if (methods.Count() > 0)
                {
                    _logger.Information($"GitHubWebHookHandlers - Type {handler.FullName} can handle webhooks");

                    // Register all the methods
                    foreach (var method in methods)
                    {
                        var eventType = method.GetParameters().First().ParameterType;
                        _coordinator.RegisterHandlerMethod(eventType, handler, method);
                        _logger.Information($"GitHubWebHookHandlers - Registered {handler.FullName} for handling event {eventType.Name}");
                    }
                }
            }
        }
    }
}