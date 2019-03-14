using System;
using System.Reflection;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface IWebhookScheduler
    {
        void QueueWebhookEventForHandling(Webhook webhook);
    }
}