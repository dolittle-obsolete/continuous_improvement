using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    public class InstallationUnregistered : IEvent
    {
        public InstallationUnregistered(long installationId, IEnumerable<string> repositories)
        {
            InstallationId = installationId;
            Repositories = repositories;
        }
        public long InstallationId { get; }
        public IEnumerable<string> Repositories { get; }
    }
}
