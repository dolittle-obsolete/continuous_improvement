using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;

namespace Domain.SourceControl.GitHub
{
    public class UnregisterInstallation : ICommand
    {
        public InstallationId Id { get; set; }
    }
}
