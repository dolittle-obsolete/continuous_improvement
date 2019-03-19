/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

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
    /// <summary>
    /// A route handler for the setup request
    /// </summary>
    public class Setup : ICanHandleRoute
    {
        readonly IExecutionContextConfigurator _executionContextConfigurator;
        readonly ITenantResolver _tenantResolver;
        readonly ICanHandleInstallationCallbacks _callbackHandler;

        /// <summary>
        /// Instantiates an instance of <see cref="Setup" />
        /// </summary>
        /// <param name="executionContextConfigurator">A configurator for the execution context</param>
        /// <param name="tenantResolver">A resolver for the tenant</param>
        /// <param name="callbackHandler">An installation callbacks handler</param>
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

        /// <inheritdoc />
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
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