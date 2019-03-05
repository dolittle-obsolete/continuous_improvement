using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Concepts.SourceControl.GitHub;
using Dolittle.Collections;
using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.SourceControl.GitHub;

namespace Read.SourceControl.GitHub
{
    public class RepositoriesEventProcessor : ICanProcessEvents
    {
        readonly IReadModelRepositoryFor<RepositoriesList> _repositoryForRepositoriesList;
        readonly IReadModelRepositoryFor<InstallationsList> _repositoryForInstallationsList;
        readonly IReadModelRepositoryFor<InstallationRepositories> _repositoryForInstallationRepositories;

        public RepositoriesEventProcessor(
            IReadModelRepositoryFor<RepositoriesList> repositoryForRepositoriesList,
            IReadModelRepositoryFor<InstallationsList> repositoryForInstallationsList,
            IReadModelRepositoryFor<InstallationRepositories> repositoryForInstallationRepositories
        )
        {
            _repositoryForRepositoriesList = repositoryForRepositoriesList;
            _repositoryForInstallationsList = repositoryForInstallationsList;
            _repositoryForInstallationRepositories = repositoryForInstallationRepositories;

            // Ensure we have the global lists
            if (_repositoryForInstallationsList.GetById(0) == null)
            {
                _repositoryForInstallationsList.Insert(new InstallationsList { Installations = new List<InstallationId>() });
            }
            if (_repositoryForRepositoriesList.GetById(0) == null)
            {
                _repositoryForRepositoriesList.Insert(new RepositoriesList { Repositories = new List<RepositoryFullName>() });
            }
        }
        
        [EventProcessor("3bd53c12-137b-ae5d-fc7a-5670f75cf402")]
        public void Process(InstallationRegistered @event)
        {
            var installationsList = _repositoryForInstallationsList.GetById(0);
            installationsList.Installations.Add(@event.InstallationId);
            _repositoryForInstallationsList.Update(installationsList);

            var repositoriesList = _repositoryForRepositoriesList.GetById(0);
            @event.Repositories.ForEach(name => repositoriesList.Repositories.Add(name));
            _repositoryForRepositoriesList.Update(repositoriesList);

            _repositoryForInstallationRepositories.Insert(new InstallationRepositories {
                Id = @event.InstallationId,
                Repositories = new List<RepositoryFullName>(@event.Repositories.Select(_ => (RepositoryFullName)_)),
            });
        }

        [EventProcessor("2d8914cf-176b-4198-ac14-dd9871c4fa3c")]
        public void Process(InstallationUnregistered @event)
        {
            var installationsList = _repositoryForInstallationsList.GetById(0);
            installationsList.Installations.Remove(@event.InstallationId);
            _repositoryForInstallationsList.Update(installationsList);

            var repositoriesList = _repositoryForRepositoriesList.GetById(0);
            @event.Repositories.ForEach(name => repositoriesList.Repositories.Remove(name));
            _repositoryForRepositoriesList.Update(repositoriesList);

            _repositoryForInstallationRepositories.Delete(new InstallationRepositories {
                Id = @event.InstallationId,
            });
        }
        
        [EventProcessor("6ba34f84-bc4e-2900-9193-960358b8d4a2")]
        public void Process(InstallationRepositoriesUpdateReceived @event)
        {
            var repositoriesList = _repositoryForRepositoriesList.GetById(0);
            @event.RepositoriesAdded.ForEach(name => {
                if (!repositoriesList.Repositories.Contains(name))
                {
                    repositoriesList.Repositories.Add(name);
                }
            });
            @event.RepositoriesRemoved.ForEach(name => {
                repositoriesList.Repositories.Remove(name);
            });
            _repositoryForRepositoriesList.Update(repositoriesList);

            var installation = _repositoryForInstallationRepositories.GetById(@event.InstallationId);
            @event.RepositoriesAdded.ForEach(name => {
                if (!installation.Repositories.Contains(name))
                {
                    installation.Repositories.Add(name);
                }
            });
            @event.RepositoriesRemoved.ForEach(name => {
                installation.Repositories.Remove(name);
            });
            _repositoryForInstallationRepositories.Update(installation);
        }

        [EventProcessor("fb15250a-ff40-4161-916a-8565bee547af")]
        public void Process(InstallationRepositoriesRefreshed @event)
        {
            var repositoriesList = _repositoryForRepositoriesList.GetById(0);
            var installation = _repositoryForInstallationRepositories.GetById(@event.InstallationId);

            installation.Repositories.ForEach(_ => {
                if (!@event.Repositories.Contains((string)_))
                {
                    repositoriesList.Repositories.Remove(_);
                }
            });
            @event.Repositories.ForEach(_ => {
                if (!installation.Repositories.Contains(_))
                {
                    repositoriesList.Repositories.Add(_);
                }
            });
            repositoriesList.Repositories = new List<RepositoryFullName>(repositoriesList.Repositories.Distinct());

            installation.Repositories = new List<RepositoryFullName>(@event.Repositories.Select(_ => (RepositoryFullName)_));

            _repositoryForRepositoriesList.Update(repositoriesList);
            _repositoryForInstallationRepositories.Update(installation);
        }
    }
}
