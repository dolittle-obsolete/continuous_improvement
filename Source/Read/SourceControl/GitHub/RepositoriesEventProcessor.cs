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
        }

        // TODO: How do we do this?
        readonly static SemaphoreSlim _lock = new SemaphoreSlim(1,1);

        RepositoriesList GetOrCreateGlobalRepositoriesList()
        {
            var list = _repositoryForRepositoriesList.GetById(0);
            if (list == null)
            {
                list = new RepositoriesList{ Repositories = new List<RepositoryFullName>() };
                _repositoryForRepositoriesList.Insert(list);
            }
            return list;
        }

        InstallationsList GetOrCreateGlobalInstallationsList()
        {
            var list = _repositoryForInstallationsList.GetById(0);
            if (list == null)
            {
                list = new InstallationsList{ Installations = new List<InstallationId>() };
                _repositoryForInstallationsList.Insert(list);
            }
            return list;
        }

        InstallationRepositories GetOrCreateInstallationById(InstallationId id)
        {
            var installation = _repositoryForInstallationRepositories.GetById(id);
            if (installation == null)
            {
                installation = new InstallationRepositories{ Id = id, Repositories = new List<RepositoryFullName>() };
                _repositoryForInstallationRepositories.Insert(installation);
            }
            return installation;
        }
        
        [EventProcessor("3bd53c12-137b-ae5d-fc7a-5670f75cf402")]
        public async void Process(InstallationRegistered @event)
        {
            await _lock.WaitAsync();

            var installationsList = GetOrCreateGlobalInstallationsList();
            installationsList.Installations.Add(@event.InstallationId);
            _repositoryForInstallationsList.Update(installationsList);

            var repositoriesList = GetOrCreateGlobalRepositoriesList();
            @event.Repositories.ForEach(name => repositoriesList.Repositories.Add(name));
            _repositoryForRepositoriesList.Update(repositoriesList);

            var installation = GetOrCreateInstallationById(@event.InstallationId);
            installation.Repositories = new List<RepositoryFullName>(@event.Repositories.Select(_ => (RepositoryFullName)_));
            _repositoryForInstallationRepositories.Update(installation);

            _lock.Release();
        }

        [EventProcessor("2d8914cf-176b-4198-ac14-dd9871c4fa3c")]
        public async void Process(InstallationUnregistered @event)
        {
            await _lock.WaitAsync();

            var installationsList = GetOrCreateGlobalInstallationsList();
            installationsList.Installations.Remove(@event.InstallationId);
            _repositoryForInstallationsList.Update(installationsList);

            var repositoriesList = GetOrCreateGlobalRepositoriesList();
            @event.Repositories.ForEach(name => repositoriesList.Repositories.Remove(name));
            _repositoryForRepositoriesList.Update(repositoriesList);

            var installation = GetOrCreateInstallationById(@event.InstallationId);
            _repositoryForInstallationRepositories.Delete(installation);

            _lock.Release();
        }
        
        [EventProcessor("6ba34f84-bc4e-2900-9193-960358b8d4a2")]
        public async void Process(InstallationRepositoriesUpdateReceived @event)
        {
            await _lock.WaitAsync();
            
            var list = GetOrCreateGlobalRepositoriesList();
            @event.RepositoriesAdded.ForEach(name => list.Repositories.Add(name));
            @event.RepositoriesRemoved.ForEach(name => list.Repositories.Remove(name));
            _repositoryForRepositoriesList.Update(list);

            var installation = GetOrCreateInstallationById(@event.InstallationId);
            @event.RepositoriesAdded.ForEach(name => installation.Repositories.Add(name));
            @event.RepositoriesRemoved.ForEach(name => installation.Repositories.Remove(name));
            _repositoryForInstallationRepositories.Update(installation);
            
            _lock.Release();
        }

        [EventProcessor("fb15250a-ff40-4161-916a-8565bee547af")]
        public async void Process(InstallationRepositoriesRefreshed @event)
        {
            await _lock.WaitAsync();

            var list = GetOrCreateGlobalRepositoriesList();
            var installation = GetOrCreateInstallationById(@event.InstallationId);

            installation.Repositories.ForEach(_ => {
                if (!@event.Repositories.Contains((string)_))
                {
                    list.Repositories.Remove(_);
                }
            });
            @event.Repositories.ForEach(_ => {
                if (!installation.Repositories.Contains(_))
                {
                    list.Repositories.Add(_);
                }
            });

            installation.Repositories = new List<RepositoryFullName>(@event.Repositories.Select(_ => (RepositoryFullName)_));

            _repositoryForRepositoriesList.Update(list);
            _repositoryForInstallationRepositories.Update(installation);

            _lock.Release();
        }
    }
}
