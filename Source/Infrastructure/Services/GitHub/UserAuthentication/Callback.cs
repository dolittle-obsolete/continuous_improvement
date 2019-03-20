/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Infrastructure.Routing;
using Infrastructure.Services.Github.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Octokit;

namespace Infrastructure.Services.Github.UserAuthentication
{
    /// <summary>
    /// A route handler for the Callback from GitHub
    /// </summary>
    public class Callback : ICanHandleRoute
    {
        readonly IGitHubCredentials _credentials;
        readonly IGitHubClientFactory _clientFactory;
        readonly IGitHubUserTokenStore _tokenStore;

        /// <summary>
        /// Instantiates an instance of <see cref="Callback" />
        /// </summary>
        /// <param name="credentials">The github credentials that are needed</param>
        /// <param name="clientFactory">A factory for creating the github client</param>
        /// <param name="tokenStore">A store for the token for the github user</param>
        public Callback(IGitHubCredentials credentials, IGitHubClientFactory clientFactory, IGitHubUserTokenStore tokenStore)
        {
            _credentials = credentials;
            _clientFactory = clientFactory;
            _tokenStore = tokenStore;
        }

        /// <inheritdoc />
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            // We should know the TenantID and user ID here
            Console.WriteLine("CODE: "+request.Query["code"]);
            Console.WriteLine("STATE: "+request.Query["state"]);

            var tokenRequest = new OauthTokenRequest(_credentials.OAuthClientID, _credentials.OAuthClientSecret, request.Query["code"]);
            var client = await _clientFactory.NewApplicationAuthenticatedClient();
            var token = await client.Oauth.CreateAccessToken(tokenRequest);

            var callback = _tokenStore.SetTokenForCurrentUser(request.Query["state"].Single(), token);
            response.StatusCode = StatusCodes.Status307TemporaryRedirect;
            response.Headers["Location"] = callback;
        }
    }
}