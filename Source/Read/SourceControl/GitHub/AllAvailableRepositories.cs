using System;
using System.Linq;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    public class AllAvailableRepositories : IQueryFor<RepositoriesList>
    {
        readonly IReadModelRepositoryFor<RepositoriesList> _repositoryForRepositoriesList;

        public AllAvailableRepositories(IReadModelRepositoryFor<RepositoriesList> repositoryForRepositoriesList)
        {
            _repositoryForRepositoriesList = repositoryForRepositoriesList;
        }

        public IQueryable<RepositoriesList> Query => _repositoryForRepositoriesList.Query;
    }
}
