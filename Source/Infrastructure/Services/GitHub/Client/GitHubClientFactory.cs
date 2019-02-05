using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Dolittle.Lifecycle;
using Microsoft.IdentityModel.Tokens;
using Octokit;

namespace Infrastructure.Services.Github.Client
{
    /// <inheritdoc />
    [Singleton]
    public class GitHubClientFactory : IGitHubClientFactory
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        const int _applicationTokenExpirationSeconds = 60*10;
        const int _installationTokenExpirationSeconds = 60*60;

        readonly IGitHubCredentials _credentials;
        readonly JwtSecurityTokenHandler _applicationTokenHandler;

        /// <summary>
        /// Instanciates a new <see cref="GitHubClientFactory">GitHubClientFactory</see>
        /// </summary>
        /// <param name="applicationUserAgent">The user-agent string to use while accessing the API</param>
        /// <param name="applicationId">The id of the application to authenticate</param>
        /// <param name="credentials">The private key to use for signing api tokens</param>
        public GitHubClientFactory(IGitHubCredentials credentials)
        {
            _credentials = credentials;
            _applicationTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <inheritdoc />
        public Task<GitHubClient> NewUnauthenticatedClient()
        {
            return Task.FromResult(new GitHubClient(new ProductHeaderValue(_credentials.ApplicationUserAgent)));
        }


        /// <inheritdoc />
        public Task<GitHubClient> NewApplicationAuthenticatedClient()
        {
            // Create a new signed JWT token for authenticating as the application
            var header = new JwtHeader(_credentials.ApplicationCredentials);

            var issued = (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;

            var payload = new JwtPayload();
            payload.Add("iat", issued);
            payload.Add("exp", issued + _applicationTokenExpirationSeconds);
            payload.Add("iss", _credentials.ApplicationId);

            var token = new JwtSecurityToken(header, payload);
            var encodedToken = _applicationTokenHandler.WriteToken(token);

            // Return a new client
            var client = new GitHubClient(new ProductHeaderValue(_credentials.ApplicationUserAgent));
            client.Credentials = new Credentials(encodedToken, AuthenticationType.Bearer);

            return Task.FromResult(client);
        }

        /// <inheritdoc />
        public async Task<GitHubClient> NewInstallationAuthenticatedClient(long installationId)
        {
            // TODO: We should do caching of installation tokens until their expiration instead of asking for a new one on every request

            // Get an installation authentication token
            var client = await NewApplicationAuthenticatedClient();
            var response = await client.GitHubApps.CreateInstallationToken(installationId);

            // Create a new client with the installation credentials
            var installationClient = new GitHubClient(new ProductHeaderValue(_credentials.ApplicationUserAgent));
            installationClient.Credentials = new Credentials(response.Token);

            return installationClient;
        }

        /// <inheritdoc />
        public Task<GitHubClient> NewUserAuthenticatedClient(OauthToken userToken)
        {
            var userClient = new GitHubClient(new ProductHeaderValue(_credentials.ApplicationUserAgent));
            userClient.Credentials = new Credentials(userToken.AccessToken);

            return Task.FromResult(userClient);
        }
    }
}