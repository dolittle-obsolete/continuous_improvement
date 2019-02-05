using System;
using System.Linq;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    public class AllInstallations : IQueryFor<InstallationsList>
    {
        readonly IReadModelRepositoryFor<InstallationsList> _repositoryForInstallationsList;

        public AllInstallations(IReadModelRepositoryFor<InstallationsList> repositoryForInstallationsList)
        {
            _repositoryForInstallationsList = repositoryForInstallationsList;
        }

        public IQueryable<InstallationsList> Query => _repositoryForInstallationsList.Query;
    }
}
