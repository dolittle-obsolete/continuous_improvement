using Infrastructure.Services.Github.Webhooks.EventPayloads;

namespace Core.Specs.SourceControl.GitHub.for_InstallationsWebhookHandler.given
{
    public class test_installation_event : InstallationEventPayload
    {
        public test_installation_event(int installationId, string action)
        {
            Installation = new Octokit.InstallationId(installationId);
            Action = action;
        }
    }
}