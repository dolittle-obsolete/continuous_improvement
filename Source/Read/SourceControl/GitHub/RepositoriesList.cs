using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    public class RepositoriesList : IReadModel
    {
        public int Id { get; } = 0;
        public IList<RepositoryFullName> Repositories { get; set; }
    }
}
