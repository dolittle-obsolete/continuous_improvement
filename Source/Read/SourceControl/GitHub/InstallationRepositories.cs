using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    public class InstallationRepositories : IReadModel
    {
        public InstallationId Id { get; set; }
        public IList<RepositoryFullName> Repositories { get; set; }
    }
}
