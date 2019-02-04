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
    public class Authenticate : ICanHandleRoute
    {
        readonly IGitHubCredentials _credentials;
        readonly IGitHubClientFactory _clientFactory;
        readonly IGitHubUserTokenStore _tokenStore;

        public Authenticate(IGitHubCredentials credentials, IGitHubClientFactory clientFactory, IGitHubUserTokenStore tokenStore)
        {
            _credentials = credentials;
            _clientFactory = clientFactory;
            _tokenStore = tokenStore;
        }

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