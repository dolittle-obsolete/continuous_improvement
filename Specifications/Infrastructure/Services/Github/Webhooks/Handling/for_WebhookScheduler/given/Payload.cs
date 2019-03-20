/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Moq;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.given
{

    public class Payload : ActivityPayload
    {
        public Payload(int value) => Number = value;
        public int Number { get; }
    }
}