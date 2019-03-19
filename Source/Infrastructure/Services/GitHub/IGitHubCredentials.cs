/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Github
{
    /// <summary>
    /// Defines the credentials needed to interact with the GitHub API
    /// </summary>
    public interface IGitHubCredentials
    {
        /// <summary>
        /// The GitHub application id 
        /// </summary>
        long ApplicationId { get; }
        /// <summary>
        /// The <see cref="SigningCredentials" />
        /// </summary>
        SigningCredentials ApplicationCredentials { get; }
        /// <summary>
        /// The application user agent
        /// </summary>
        string ApplicationUserAgent { get; }
        /// <summary>
        /// The webhook secret
        /// </summary>
        byte[] WebhookSecret { get; }
        /// <summary>
        /// The OAuth client id
        /// </summary>
        string OAuthClientID { get; }
        /// <summary>
        /// The OAuth client secret
        /// </summary>
        string OAuthClientSecret { get; }
    }
}