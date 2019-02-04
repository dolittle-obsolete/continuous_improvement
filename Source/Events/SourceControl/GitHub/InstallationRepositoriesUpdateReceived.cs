using System;
using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    public class InstallationRepositoriesUpdateReceived : IEvent
    {
        public InstallationRepositoriesUpdateReceived(long installationId, IEnumerable<string> repositoriesAdded, IEnumerable<string> repositoriesRemoved)
        {
            InstallationId = installationId;
            RepositoriesAdded = repositoriesAdded;
            RepositoriesRemoved = repositoriesRemoved;
        }

        public long InstallationId { get; }
        public IEnumerable<string> RepositoriesAdded { get; }
        public IEnumerable<string> RepositoriesRemoved { get; }
    }
}
