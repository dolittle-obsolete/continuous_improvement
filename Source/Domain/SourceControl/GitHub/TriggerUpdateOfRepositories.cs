using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;

namespace Domain.SourceControl.GitHub
{
    public class TriggerUpdateOfRepositories : ICommand
    {
        public IEnumerable<InstallationId> InstallationIds { get; set; }
    }
}
