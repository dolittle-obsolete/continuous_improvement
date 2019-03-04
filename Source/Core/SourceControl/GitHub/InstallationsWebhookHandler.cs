
using System;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands.Coordination;
using Dolittle.ReadModels;
using Domain.SourceControl.GitHub;
using Infrastructure.Services.Github;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Infrastructure.Services.Github.Webhooks.Handling;
using Octokit;
using Read.SourceControl.GitHub;

namespace Core.SourceControl.GitHub
{
    public class InstallationsWebhookHandler : ICanHandleGitHubWebhooks
    {
        readonly ICommandCoordinator _commandCoordinator;

        public InstallationsWebhookHandler(
            ICommandCoordinator commandCoordinator
        )
        {
            _commandCoordinator = commandCoordinator;
        }

        void On(InstallationEventPayload payload)
        {
            if (payload.Action.Equals("deleted", StringComparison.OrdinalIgnoreCase))
            {
                _commandCoordinator.Handle(new UnregisterInstallation {
                    Id = payload.Installation.Id,
                });
            }
        }

        void On(InstallationRepositoriesEventPayload payload)
        {
            // TODO: Result?
            _commandCoordinator.Handle(new UpdateInstallationRepositories {
                Id = payload.Installation.Id,
                RepositoriesAdded = payload.RepositoriesAdded.Select(_ => (RepositoryFullName)_.FullName),
                RepositoriesRemoved = payload.RepositoriesRemoved.Select(_ => (RepositoryFullName)_.FullName)
            });
        }
    }
}