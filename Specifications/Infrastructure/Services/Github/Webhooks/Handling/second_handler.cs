/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Booting;
using Dolittle.Logging;
using Dolittle.Types;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Moq;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{

    public interface second_handler : ICanHandleGitHubWebhooks
    {
        void On(InstallationEventPayload payload);
        void On(InstallationRepositoriesEventPayload payload);
        void On(DeleteEventPayload payload);
    }
}