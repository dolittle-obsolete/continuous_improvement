/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Linq;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_WebhookScheduler.given
{
    public static class a
    {
        public static Webhook webhook_from(Payload payload)
        {
            var type = typeof(first_handler);
            return new Webhook(new HandlerMethod(type, type.GetMethods().First()), payload);
        }
    }
}