using System;
using System.IO;
using System.Text;
using Dolittle.Lifecycle;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Infrastructure.Services.Github
{
    /// <inheritdoc />
    [Singleton]
    public class GitHubCredentials : IGitHubCredentials
    {
        const string GITHUB_APPLICATION_ID_VAR = "GITHUB_APP_ID";
        const string GITHUB_PRIVATE_KEY_PATH_VAR = "GITHUB_PRIVATE_KEY_PATH";
        const string GITHUB_HOOK_SECRET_VAR = "GITHUB_HOOK_SECRET";
        const string GITHUB_OAUTH_ID_VAR = "GITHUB_OAUTH_ID";
        const string GITHUB_OAUTH_SECRET_VAR = "GITHUB_OAUTH_SECRET";
        const string _applicationUserAgent = "dolittle-continuous-improvement";

        internal GitHubCredentials() {
            ThrowIfEnvironmentalVariablesNotSet();

            // Get the application id from the environment
            ApplicationId = long.Parse(Environment.GetEnvironmentVariable(GITHUB_APPLICATION_ID_VAR));

            // Read the signing key from disk
            var privateKeyPath = Environment.GetEnvironmentVariable(GITHUB_PRIVATE_KEY_PATH_VAR);
            using (var stream = new StreamReader(privateKeyPath))
            {
                var reader = new PemReader(stream);
                var keyPair = (AsymmetricCipherKeyPair)reader.ReadObject();

                var rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);
                var key = new RsaSecurityKey(rsaParams);
                ApplicationCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            }

            // Keep webhook secret in memory as bytes
            WebhookSecret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(GITHUB_HOOK_SECRET_VAR));

            // OAuth data
            OAuthClientID = Environment.GetEnvironmentVariable(GITHUB_OAUTH_ID_VAR);
            OAuthClientSecret = Environment.GetEnvironmentVariable(GITHUB_OAUTH_SECRET_VAR);
        }

        void ThrowIfEnvironmentalVariablesNotSet()
        {
            if (Environment.GetEnvironmentVariable(GITHUB_APPLICATION_ID_VAR) == null)
                throw new MissingCredentialEnvironmentVariable(GITHUB_APPLICATION_ID_VAR);

            if (Environment.GetEnvironmentVariable(GITHUB_PRIVATE_KEY_PATH_VAR) == null)
                throw new MissingCredentialEnvironmentVariable(GITHUB_PRIVATE_KEY_PATH_VAR);

            if (Environment.GetEnvironmentVariable(GITHUB_HOOK_SECRET_VAR) == null)
                throw new MissingCredentialEnvironmentVariable(GITHUB_HOOK_SECRET_VAR);

            if (Environment.GetEnvironmentVariable(GITHUB_OAUTH_ID_VAR) == null)
                throw new MissingCredentialEnvironmentVariable(GITHUB_OAUTH_ID_VAR);

            if (Environment.GetEnvironmentVariable(GITHUB_OAUTH_SECRET_VAR) == null)
                throw new MissingCredentialEnvironmentVariable(GITHUB_OAUTH_SECRET_VAR);
        }

        /// <inheritdoc />
        public long ApplicationId { get; private set; }
        /// <inheritdoc />
        public SigningCredentials ApplicationCredentials { get; private set; }
        /// <inheritdoc />
        public string ApplicationUserAgent => _applicationUserAgent;
        /// <inheritdoc />
        public byte[] WebhookSecret { get; private set; }

        public string OAuthClientID { get; private set; }

        public string OAuthClientSecret { get; private set; }
    }
}