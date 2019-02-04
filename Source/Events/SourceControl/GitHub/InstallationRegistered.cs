using System;
using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    public class InstallationRegistered : IEvent
    {
        public InstallationRegistered(long installationId, string targetType, string targetAccount, IEnumerable<string> repositories)
        {
            InstallationId = installationId;
            TargetType = targetType;
            TargetAccount = targetAccount;
            Repositories = repositories;
        }

        public long InstallationId { get; }
        public string TargetType { get; }
        public string TargetAccount { get; }
        public IEnumerable<string> Repositories { get; }
    }
}
