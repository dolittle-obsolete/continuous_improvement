/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Machine.Specifications;
using Moq;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookProcessor.given
{
    public class a_webhook_processor
    {
        protected static IWebhookProcessor processor;
        protected static Mock<IContainer> container;

        Establish context = () =>
        {
            container = new Mock<IContainer>();
            processor = new WebhookProcessor(container.Object);
        };

        protected static Webhook BuildWebhook<T>(ActivityPayload payload)where T : ICanHandleGitHubWebhooks
        {
            return new Webhook(HandlerMethod.GetUsableHandlerMethodsFrom(typeof(T)).First(), payload);
        }
    }
}