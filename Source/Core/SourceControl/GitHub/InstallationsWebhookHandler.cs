/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;
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
    /// <summary>
    /// An implementation of <see cref="ICanHandleGitHubWebhooks" /> 
    /// </summary>
    public class InstallationsWebhookHandler : ICanHandleGitHubWebhooks
    {
        /// <summary>
        /// A string constant for the deleted action
        /// </summary>
        public const string DELETED = "deleted";
        readonly ICommandCoordinator _commandCoordinator;

        /// <summary>
        /// Instantiates an instance of <see cref="InstallationsWebhookHandler" />
        /// </summary>
        /// <param name="commandCoordinator">A <see cref="ICommandCoordinator" /> to allow <see cref="ICommand">Commands</see></param>
        public InstallationsWebhookHandler(
            ICommandCoordinator commandCoordinator
        )
        {
            _commandCoordinator = commandCoordinator;
        }

        /// <summary>
        /// Handles the <see cref="InstallationEventPayload" />
        /// </summary>
        /// <param name="payload">Instance of <see cref="InstallationEventPayload" /></param>
        public void On(InstallationEventPayload payload)
        {
            if (payload.Action.Equals(DELETED, StringComparison.OrdinalIgnoreCase))
            {
                _commandCoordinator.Handle(new UnregisterInstallation
                {
                    Id = payload.Installation.Id,
                });
            }
        }

        /// <summary>
        /// Handles the <see cref="InstallationRepositoriesEventPayload" />
        /// </summary>
        /// <param name="payload">Instance of <see cref="InstallationRepositoriesEventPayload" /></param>
        public void On(InstallationRepositoriesEventPayload payload)
        {
            // TODO: Result?
            _commandCoordinator.Handle(new UpdateInstallationRepositories
            {
                Id = payload.Installation.Id,
                    RepositoriesAdded = payload.RepositoriesAdded.Select(_ => (RepositoryFullName)_.FullName),
                    RepositoriesRemoved = payload.RepositoriesRemoved.Select(_ => (RepositoryFullName)_.FullName)
            });
        }
    }
}