using System;
using System.Reflection;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface IWebhookCoordinator
    {
        void RegisterHandlerMethod(Type payloadType, Type handler, MethodInfo method);

        bool WillHandle<T>() where T : ActivityPayload;

        void HandleWebhookPayload(ActivityPayload payload, Guid deliveryId);
    }
}