/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.SourceControl.GitHub;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Allows <see cref="Installation">Installations</see> to be managed against an Application
    /// </summary>
    public class Application : AggregateRoot
    {
        /// <summary>
        /// Instantiates an instance of <see cref="Application" />
        /// </summary>
        /// <param name="id">The Id of the Application</param>
        public Application(EventSourceId id) : base(id) {}

        Dictionary<InstallationId, Installation> _installations = new Dictionary<InstallationId, Installation>();


        void On(InstallationRegistered @event)
        {
            _installations[@event.InstallationId] = new Installation(
                @event.InstallationId,
                @event.TargetType,
                @event.TargetAccount,
                @event.Repositories.Select(_ => (RepositoryFullName)_)
            );
        }

        void On(InstallationUnregistered @event)
        {
            _installations.Remove(@event.InstallationId);
        }

        void On(InstallationRepositoriesUpdateReceived @event)
        {
            var installation = _installations[@event.InstallationId];
            _installations[@event.InstallationId] = new Installation(
                installation.Id,
                installation.TargetType,
                installation.TargetAccount,
                installation.Repositories.Where(_ => !@event.RepositoriesRemoved.Contains((string)_)).Union(@event.RepositoriesAdded.Select(_ => (RepositoryFullName)_))
            );
        }

        void On(InstallationRepositoriesRefreshed @event)
        {
            var installation = _installations[@event.InstallationId];
            _installations[@event.InstallationId] = new Installation(
                installation.Id,
                installation.TargetType,
                installation.TargetAccount,
                @event.Repositories.Select(_ => (RepositoryFullName)_)
            );
        }
        /// <summary>
        /// Registers an installation with this Application
        /// </summary>
        /// <param name="id">The id of the Installation</param>
        /// <param name="targetType">The type of the GitHub account</param>
        /// <param name="targetAccount">The login details of the GitHub account</param>
        /// <param name="repositories">A collection of repositories to be registered</param>
        public void RegisterInstallation(InstallationId id, AccountType targetType, AccountLogin targetAccount, IEnumerable<RepositoryFullName> repositories)
        {
            if (_installations.ContainsKey(id)) throw new Exception("Installation already registered!");

            Apply(new InstallationRegistered(
                id,
                targetType,
                targetAccount,
                repositories.Select(fullName => (string)fullName)
            ));
        }

        /// <summary>
        /// Unregisteres an installation from this Applicaition
        /// </summary>
        /// <param name="id">The id of the <see cref="Installation" /> being unregistered</param>
        public void UnregisterInstallation(InstallationId id)
        {
            if (!_installations.ContainsKey(id)) throw new Exception("Installation is not registered!");

            Apply(new InstallationUnregistered(
                id,
                _installations[id].Repositories.Select(_ => _.Value)
            ));
        }

        /// <summary>
        /// Updates the repositories that are associated with this <see cref="Installation" />
        /// </summary>
        /// <param name="id">The id of the <see cref="Installation" /> being updated</param>
        /// <param name="repositoriesAdded">A collection of repositories being added</param>
        /// <param name="repositoriesRemoved">A collection of repositories to be removed</param>
        public void UpdateInstallationRepositories(InstallationId id, IEnumerable<RepositoryFullName> repositoriesAdded, IEnumerable<RepositoryFullName> repositoriesRemoved)
        {
            if (!_installations.ContainsKey(id)) throw new Exception("Installation not registered on update!");

            Apply(new InstallationRepositoriesUpdateReceived(
                id,
                repositoriesAdded.Select(fullName => (string)fullName),
                repositoriesRemoved.Select(fullName => (string)fullName)
            ));
        }

        /// <summary>
        /// Refreshes the list of repositories with this installation
        /// </summary>
        /// <param name="id">The id of the installation being refreshed</param>
        /// <param name="repositories">A collection of repositories that are associated with this <see cref="Installation" /></param>
        public void RefreshInstallationRepositories(InstallationId id, IEnumerable<RepositoryFullName> repositories)
        {
            if (!_installations.ContainsKey(id)) throw new Exception("Installation not registered on update!");

            Apply(new InstallationRepositoriesRefreshed(
                id,
                repositories.Select(_ => (string)_)
            ));
        }
    }
}
