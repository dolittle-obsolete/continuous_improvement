
using System.Collections.Generic;
using Octokit;

#pragma warning disable 1591
namespace Infrastructure.Services.Github.Webhooks.EventPayloads
{
    
    public class InstallationRepositoriesEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public string RepositorySelection { get; protected set; }
        public IReadOnlyList<Repository> RepositoriesAdded { get; protected set; }
        public IReadOnlyList<Repository> RepositoriesRemoved { get; protected set; }
    }
}
#pragma warning restore 1591