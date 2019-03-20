/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;
using Infrastructure.Services.Github.Client;
using Infrastructure.Services.Github.UserAuthentication;

namespace Infrastructure.Services.Github
{
    /// <summary>
    /// Represents bindings for the GitHub client
    /// </summary>
    public class GitHubBindings : ICanProvideBindings
    {
        /// <inheritdoc />
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IGitHubCredentials>().To(new GitHubCredentials());

            builder.Bind<IGitHubClientFactory>().To<GitHubClientFactory>();

            builder.Bind<IGitHubUserTokenStore>().To<GitHubUserTokenStore>();
        }
    }
}