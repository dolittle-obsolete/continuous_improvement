using Concepts.SourceControl.GitHub;
using Dolittle.Collections;
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
    public class ApplicationCommandHandler : ICanHandleCommands
    {
        readonly IAggregateRootRepositoryFor<Application>  _aggregateRootRepoForInstallations;
        readonly IGitHubClientFactory _githubClientFactory;

        public ApplicationCommandHandler(
            IAggregateRootRepositoryFor<Application>  aggregateRootRepoForInstallations,
            IGitHubClientFactory gitHubClientFactory
        )
        {
             _aggregateRootRepoForInstallations =  aggregateRootRepoForInstallations;
             _githubClientFactory = gitHubClientFactory;
        }

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

        public void Handle(UnregisterInstallation cmd)
        {
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            aggregateRoot.UnegisterInstallation(cmd.Id);
        }

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

        public void Handle(TriggerUpdateOfRepositories cmd)
        {
            var aggregateRoot = _aggregateRootRepoForInstallations.GetApplication();
            cmd.InstallationIds.ForEach(id => {
                try
                {
                    var client = _githubClientFactory.NewInstallationAuthenticatedClient(id).Result;
                    var repositories = client.GitHubApps.Installation.GetAllRepositoriesForCurrent().Result.Repositories;
                    aggregateRoot.RefreshedInstallationRepositories(id, repositories.Select(_ => (RepositoryFullName)_.FullName));
                }
                catch (NotFoundException)
                {
                    // This means that the installation no longer exists, i.e. has been removed
                    aggregateRoot.UnegisterInstallation(id);
                }
            });
        }
    }
}
