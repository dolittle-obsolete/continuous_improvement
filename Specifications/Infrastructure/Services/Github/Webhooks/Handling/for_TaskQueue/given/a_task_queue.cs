/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TaskQueue.given
{
    public class a_task_queue
    {
        public static TaskQueue get_task_queue() => new TaskQueue();
    }
}