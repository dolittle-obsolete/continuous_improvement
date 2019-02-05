using System;
using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.SourceControl.GitHub;
using System.Linq;

namespace Domain.SourceControl.GitHub
{
    public class Installation
    {
        public Installation(InstallationId id, AccountType targetType, AccountLogin targetAccount, IEnumerable<RepositoryFullName> repositories)
        {
            Id = id;
            TargetType = targetType;
            TargetAccount = targetAccount;
            Repositories = repositories;
        }

        public InstallationId Id { get; }
        public AccountType TargetType { get; }
        public AccountLogin TargetAccount { get; }
        public IEnumerable<RepositoryFullName> Repositories { get; }
    }
}
