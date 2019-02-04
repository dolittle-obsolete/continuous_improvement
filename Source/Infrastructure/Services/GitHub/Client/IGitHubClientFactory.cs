/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Octokit;

namespace Infrastructure.Services.Github.Client
{
    /// <summary>
    /// Represents a factory that provides <see cref="GitHubClient">GitHub client</see>s with authentication properties set.
    /// </summary>
    /// <remarks>
    /// These clients might have tokens that expire at a point in time, so they should be considered ephemeral, and a new client should created for each request.
    /// </remarks>
    public interface IGitHubClientFactory
    {
        /// <summary>
        /// Creates a new <see cref="GitHubClient">GitHub client</see> that is not authenticated.
        /// </summary>
        /// <returns>A new <see cref="GitHubClient">client</see></returns>
        Task<GitHubClient> NewUnauthenticatedClient();

        /// <summary>
        /// Creates a new <see cref="GitHubClient">GitHub client</see> that is autenticated as the application, not bound to a specific installation.
        /// </summary>
        /// <returns>A new <see cref="GitHubClient">client</see></returns>
        Task<GitHubClient> NewApplicationAuthenticatedClient();

        /// <summary>
        /// Creates a new <see cref="GitHubClient">GitHub client</see> that is autenticated as a specific installation of the application.
        /// </summary>
        /// <returns>A new <see cref="GitHubClient">client</see></returns>
        Task<GitHubClient> NewInstallationAuthenticatedClient(long installationId);

        /// <summary>
        /// Creates a new <see cref="GitHubClient">GitHub client</see> that is autenticated as a user.
        /// </summary>
        /// <returns>A new <see cref="GitHubClient">client</see></returns>
        Task<GitHubClient> NewUserAuthenticatedClient(OauthToken userToken);
    }
}