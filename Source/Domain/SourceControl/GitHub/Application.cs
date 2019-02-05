using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.SourceControl.GitHub;

namespace Domain.SourceControl.GitHub
{
    public class Application : AggregateRoot
    {
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
        public void UpdateInstallationRepositories(InstallationId id, IEnumerable<RepositoryFullName> repositoriesAdded, IEnumerable<RepositoryFullName> repositoriesRemoved)
        {
            if (!_installations.ContainsKey(id)) throw new Exception("Installation not registered on update!");

            Apply(new InstallationRepositoriesUpdateReceived(
                id,
                repositoriesAdded.Select(fullName => (string)fullName),
                repositoriesRemoved.Select(fullName => (string)fullName)
            ));
        }

        public void RefreshedInstallationRepositories(InstallationId id, IEnumerable<RepositoryFullName> repositories)
        {
            if (!_installations.ContainsKey(id)) throw new Exception("Installation not registered on update!");

            Apply(new InstallationRepositoriesRefreshed(
                id,
                repositories.Select(_ => (string)_)
            ));
        }
    }

    public static class ApplicationRepositoryExtensions
    {
        static readonly EventSourceId SINGLETON_ID = new Guid("ff042dd1-d012-46cb-b5ed-867ba80d54e3");

        public static Application GetApplication(this IAggregateRootRepositoryFor<Application> repository)
        {
            return repository.Get(SINGLETON_ID);
        }
    }
}
