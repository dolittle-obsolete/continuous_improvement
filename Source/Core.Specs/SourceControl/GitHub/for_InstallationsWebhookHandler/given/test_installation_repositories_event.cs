using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Github.Webhooks.EventPayloads;

namespace Core.Specs.SourceControl.GitHub.for_InstallationsWebhookHandler.given
{
    public class test_installation_repositories_event : InstallationRepositoriesEventPayload
    {
        public test_installation_repositories_event(int installationId, IEnumerable<string> toAdd, IEnumerable<string> toRemove)
        {
            this.Installation = new Octokit.InstallationId(installationId);
            this.RepositoriesAdded = toAdd.Select(_ => new test_repository(_)).ToList();
            this.RepositoriesRemoved = toRemove.Select(_ => new test_repository(_)).ToList();
        }

        private class test_repository : Octokit.Repository
        {
            public test_repository(string name)
            {
                this.FullName = name;
            }
        }
    }
}