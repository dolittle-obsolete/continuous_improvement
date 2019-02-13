using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dolittle.AspNetCore.Execution;
using Dolittle.Commands.Coordination;
using Dolittle.Logging;
using Dolittle.Security;
using Dolittle.Tenancy;
using Infrastructure.Routing;
using Infrastructure.Services.Github.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Octokit;

namespace Infrastructure.Services.Github.Installation
{
    public class Setup : ICanHandleRoute
    {
        readonly IExecutionContextConfigurator _executionContextConfigurator;
        readonly ITenantResolver _tenantResolver;
        readonly ICanHandleInstallationCallbacks _callbackHandler;

        public Setup(
            IExecutionContextConfigurator executionContextConfigurator,
            ITenantResolver tenantResolver,
            ICanHandleInstallationCallbacks callbackHandler
        )
        {
            _executionContextConfigurator = executionContextConfigurator;
            _tenantResolver = tenantResolver;
            _callbackHandler = callbackHandler;
        }

        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            // TODO: This endpoint should be secured like all the others, so that when the final request gets here, we should know the tenant

            _executionContextConfigurator.ConfigureFor(_tenantResolver.Resolve(request), Guid.NewGuid(), ClaimsPrincipal.Current.ToClaims());
                
            var installationId = long.Parse(request.Query["installation_id"].Single());
            switch (request.Query["setup_action"].Single())
            {
                case "install":
                    _callbackHandler.Install(installationId, response);
                    break;
                case "update":
                    _callbackHandler.Update(installationId, response);
                    break;
            }

            await Task.CompletedTask;
        }
    }
}