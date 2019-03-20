/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Octokit;

namespace Infrastructure.Services.Github.UserAuthentication
{
    /// <summary>
    /// Defines a Store for the github user token for the current user
    /// </summary>
    public interface IGitHubUserTokenStore
    {
        /// <summary>
        /// Generates a state string for the current user
        /// </summary>
        /// <param name="callback"></param>
        string GenerateStateForCurrentUser(string callback);
        /// <summary>
        /// Sets the token and state for the current user
        /// </summary>
        /// <param name="state">The state to set</param>
        /// <param name="token">The token to set</param>
        /// <returns></returns>
        string SetTokenForCurrentUser(string state, OauthToken token);
        /// <summary>
        /// Indicates whether there is a token for the current user
        /// </summary>
        /// <returns>true if there is a token, false otherwise</returns>
        bool HasTokenForCurrentUser();
        /// <summary>
        /// Gets the OAuth token for the current user
        /// </summary>
        /// <returns>The OAuth token</returns>
        OauthToken GetTokenForCurrentUser();
    }
}