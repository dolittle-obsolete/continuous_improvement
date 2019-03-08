
using System.Collections.Generic;
using Octokit;

#pragma warning disable 1591
namespace Infrastructure.Services.Github.Webhooks.EventPayloads
{
    
    public class InstallationEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public IReadOnlyList<Repository> Repositories { get; protected set; }
    }
}
#pragma warning restore 1591