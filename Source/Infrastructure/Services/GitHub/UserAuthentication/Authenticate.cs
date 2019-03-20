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
    /// A route handler for Authentication
    /// </summary>
    public class Authenticate : ICanHandleRoute
    {
        readonly IGitHubCredentials _credentials;
        readonly IGitHubClientFactory _clientFactory;
        readonly IGitHubUserTokenStore _tokenStore;

        /// <summary>
        /// Instantiates an instance of <see cref="Authenticate" />
        /// </summary>
        /// <param name="credentials">The github credentials that are needed</param>
        /// <param name="clientFactory">A factory for creating the github client</param>
        /// <param name="tokenStore">A store for the token for the github user</param>
        public Authenticate(IGitHubCredentials credentials, IGitHubClientFactory clientFactory, IGitHubUserTokenStore tokenStore)
        {
            _credentials = credentials;
            _clientFactory = clientFactory;
            _tokenStore = tokenStore;
        }

        /// <inheritdoc />
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            // We should know the TenantID and user ID here
            var loginRequest = new OauthLoginRequest(_credentials.OAuthClientID) { State = _tokenStore.GenerateStateForCurrentUser(request.Query["callback"].Single()) };
            var client = await _clientFactory.NewUnauthenticatedClient();
            
            response.StatusCode = StatusCodes.Status307TemporaryRedirect;
            response.Headers["Location"] = client.Oauth.GetGitHubLoginUrl(loginRequest).ToString();
        }
    }
}