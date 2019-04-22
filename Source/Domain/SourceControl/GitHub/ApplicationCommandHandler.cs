/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.SourceControl;
using Concepts.SourceControl.GitHub;
using Dolittle.Collections;
using Dolittle.Commands;
using Dolittle.Commands.Handling;
using Dolittle.Domain;
using Events.SourceControl.GitHub;
using Infrastructure.Services.Github;
using Infrastructure.Services.Github.Client;
using Octokit;
using System.Collections.Generic;
using System.Linq;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Handles <see cref="ICommand">Commands</see> for an <see cref="Application" />
    /// </summary>
    public class ApplicationCommandHandler : ICanHandleCommands
    {
        readonly IAggregateRootRepositoryFor<Application>  _aggregateRootRepoForInstallations;
        readonly IGitHubClientFactory _githubClientFactory;

        /// <summary>
        /// Instantiates an instance of <see cref="ApplicationCommandHandler" /> 
        /// </summary>
        /// <param name="aggregateRootRepoForInstallations">An aggregate root repository for <see cref="Installation">installations</see></param>
        /// <param name="gitHubClientFactory">A factory to create a <see cref="GitHubClient" /></param>
        public ApplicationCommandHandler(
            IAggregateRootRepositoryFor<Application>  aggregateRootRepoForInstallations,
            IGitHubClientFactory gitHubClientFactory
        )
        {
             _aggregateRootRepoForInstallations =  aggregateRootRepoForInstallations;
             _githubClientFactory = gitHubClientFactory;
        }

        /// <summary>
        /// Handles a <see cref="RegisterInstallation" /> command
        /// </summary>
        /// <param name="cmd">a <see cref="RegisterInstallation" /> command</param>
        public void Handle(RegisterInstallation cmd)
        {
            // Get installation details
            var client = _githubClientFactory.NewApplicationAuthenticatedClient().Result;
            var installation = client.GitHubApps.GetInstallationForCurrent(cmd.Id).Result;

            // Get installation repositories
            client = _githubClientFactory.NewInstallationAuthenticatedClient(cmd.Id).Result;
            var repositories = client.GitHubApps.Installation.GetAllRepositoriesForCurrent().Result.Repositories;

            // Register the installation
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            aggregateRoot.RegisterInstallation(
                cmd.Id,
                installation.TargetType.StringValue,
                installation.Account.Login,
                repositories.Select(repository => (RepositoryFullName)repository.FullName)
            );
        }

        /// <summary>
        /// Handles an <see cref="UnregisterInstallation" /> command
        /// </summary>
        /// <param name="cmd">an <see cref="UnregisterInstallation" /> command</param>
        public void Handle(UnregisterInstallation cmd)
        {
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            aggregateRoot.UnregisterInstallation(cmd.Id);
        }

        /// <summary>
        /// Handles an <see cref="UpdateInstallationRepositories" /> command
        /// </summary>
        /// <param name="cmd">an <see cref="UpdateInstallationRepositories" /> command</param>
        public void Handle(UpdateInstallationRepositories cmd)
        {
            // Trigger the update
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            aggregateRoot.UpdateInstallationRepositories(
                cmd.Id,
                cmd.RepositoriesAdded,
                cmd.RepositoriesRemoved
            );
        }

        /// <summary>
        /// Handles an <see cref="TriggerUpdateOfRepositories" /> command
        /// </summary>
        /// <param name="cmd">an <see cref="TriggerUpdateOfRepositories" /> command</param>
        public void Handle(TriggerUpdateOfRepositories cmd)
        {
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            cmd.InstallationIds.ForEach(id => {
                try
                {
                    var client = _githubClientFactory.NewInstallationAuthenticatedClient(id).Result;
                    var repositories = client.GitHubApps.Installation.GetAllRepositoriesForCurrent().Result.Repositories;
                    aggregateRoot.RefreshInstallationRepositories(id, repositories.Select(_ => (RepositoryFullName)_.FullName));
                }
                catch (NotFoundException)
                {
                    // This means that the installation no longer exists, i.e. has been removed
                    aggregateRoot.UnregisterInstallation(id);
                }
            });
        }
    }
}
