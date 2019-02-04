using Octokit;

namespace Infrastructure.Services.Github.UserAuthentication
{
    public interface IGitHubUserTokenStore
    {
        string GenerateStateForCurrentUser(string callback);
        string SetTokenForCurrentUser(string state, OauthToken token);
        bool HasTokenForCurrentUser();
        OauthToken GetTokenForCurrentUser();
    }
}