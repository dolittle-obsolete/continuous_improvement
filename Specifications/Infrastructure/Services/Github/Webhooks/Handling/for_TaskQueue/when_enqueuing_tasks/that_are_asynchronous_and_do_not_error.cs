/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TaskQueue.when_enqueuing_tasks
{
    [Subject(typeof(TaskQueue),"Enqueue")]
    public class that_are_asynchronous_and_do_not_error : given.a_task_queue
    {
        static List<int> results;
        static List<Task> tasks;
        static List<int> values;
        static TaskQueue queue;

        Establish context = () => 
        {
            queue = get_task_queue();
            values = Enumerable.Range(0,1000).ToList();
            tasks = new List<Task>();
            results = new List<int>();
        };

        Because of = () => 
        {
            values.ForEach(v => enqueue(v));
        };

        It should_have_processed_all_the_tasks_in_sequence = async () => 
        {
            await Task.WhenAll(tasks).ConfigureAwait(false);
            results.ShouldEqual(values);
        };

        static void enqueue(int value)
        {
            tasks.Add(queue.Enqueue(async() => {
                await Task.Delay(1);
                results.Add(value);
                return;
            }));
        }   
    }
}