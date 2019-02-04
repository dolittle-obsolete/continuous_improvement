using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    public class InstallationsList : IReadModel
    {
        public int Id { get; } = 0;
        public IList<InstallationId> Installations { get; set; }
    }
}
