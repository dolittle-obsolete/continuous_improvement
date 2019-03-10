using System;
using System.Reflection;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface IWebhookCoordinator
    {
        bool WillHandle<T>() where T : ActivityPayload;

        void HandleWebhookPayload(ActivityPayload payload, Guid deliveryId);
    }
}