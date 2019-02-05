using System;
using System.Reflection;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface IWebhookScheduler
    {
        void QueueWebhookEventForHandling(Type handler, MethodInfo method, ActivityPayload payload);
    }
}