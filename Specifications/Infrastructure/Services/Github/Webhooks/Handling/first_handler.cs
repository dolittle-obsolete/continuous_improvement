/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Infrastructure.Services.Github.Webhooks.EventPayloads;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface first_handler : ICanHandleGitHubWebhooks
    {
        void On(CreateEventPayload payload);
        void On(DeleteEventPayload payload);
    }
}