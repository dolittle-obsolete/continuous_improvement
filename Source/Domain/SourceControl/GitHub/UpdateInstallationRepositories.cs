using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;

namespace Domain.SourceControl.GitHub
{
    public class UpdateInstallationRepositories : ICommand
    {
        public InstallationId Id { get; set; }
        public IEnumerable<RepositoryFullName> RepositoriesAdded { get; set; }
        public IEnumerable<RepositoryFullName> RepositoriesRemoved { get; set; }
    }
}
