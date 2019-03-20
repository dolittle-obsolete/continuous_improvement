/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Serialization.Json;
using Infrastructure.Routing;
using Infrastructure.Services.Github.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Octokit;
using Octokit.Internal;

namespace Infrastructure.Services.Github.UserAuthentication
{
    /// <summary>
    /// A route to handle requests for getting the installations for a user
    /// </summary>
    public class GetInstallationsForUserProxy : ICanHandleRoute
    {
        readonly IGitHubClientFactory _clientFactory;
        readonly IGitHubUserTokenStore _tokenStore;
        readonly ISerializer _serializer;

        /// <summary>
        /// Instantiates an instance of <see cref="GetInstallationsForUserProxy" />
        /// </summary>
        /// <param name="clientFactory">A factory for creating the github client</param>
        /// <param name="tokenStore">A store for the token for the github user</param>
        /// <param name="serializer">A serializer</param>
        public GetInstallationsForUserProxy(IGitHubClientFactory clientFactory, IGitHubUserTokenStore tokenStore, ISerializer serializer)
        {
            _clientFactory = clientFactory;
            _tokenStore = tokenStore;
            _serializer = serializer;
        }

        /// <inheritdoc />
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            if (_tokenStore.HasTokenForCurrentUser())
            {
                var client = await _clientFactory.NewUserAuthenticatedClient(_tokenStore.GetTokenForCurrentUser());
                var installations = await client.GitHubApps.GetAllInstallationsForCurrentUser();

                var mapped = installations.Installations.Select(_ => new Installation{
                    Id = _.Id,
                    Type = _.Account.Type.Value == AccountType.User ? "user" : "organization",
                    Login = _.Account.Login,
                    AvatarUrl = _.Account.AvatarUrl
                });
                
                using (var data = _serializer.ToJsonStream(mapped))
                {
                    response.StatusCode = StatusCodes.Status200OK;
                    response.ContentType = "application/json";
                    response.ContentLength = data.Length;
                    await data.CopyToAsync(response.Body);
                }
            }
            else
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        struct Installation
        {
            public long Id;
            public string Type;
            public string Login;
            public string AvatarUrl;
        }
    }
}