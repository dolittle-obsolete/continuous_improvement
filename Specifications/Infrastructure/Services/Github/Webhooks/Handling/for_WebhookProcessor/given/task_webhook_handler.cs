/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Machine.Specifications;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookProcessor.given
{
    public class task_webhook_handler : ICanHandleGitHubWebhooks
    {
        public ActivityPayload CalledWithPayload { get; private set; }
        public bool WasCalled => CalledWithPayload != null;
        public virtual async Task On(ActivityPayload payload)
        {
            CalledWithPayload = payload;
            await Task.Delay(100);
        }
    }
}