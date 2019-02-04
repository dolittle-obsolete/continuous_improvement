using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Github
{
    public interface IGitHubCredentials
    {
        long ApplicationId { get; }
        SigningCredentials ApplicationCredentials { get; }
        string ApplicationUserAgent { get; }
        byte[] WebhookSecret { get; }
        string OAuthClientID { get; }
        string OAuthClientSecret { get; }
    }
}