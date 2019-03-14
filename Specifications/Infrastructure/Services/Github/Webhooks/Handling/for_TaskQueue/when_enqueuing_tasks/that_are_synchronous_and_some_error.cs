/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TaskQueue.when_enqueuing_tasks
{
    [Subject(typeof(TaskQueue),"Enqueue")]
    public class that_are_synchronous_and_some_error : given.a_task_queue
    {
        static List<int> results;
        static List<Task> tasks;
        static List<int> values;
        static List<int> errors;
        static TaskQueue queue;

        Establish context = () => 
        {
            queue = get_task_queue();
            values = Enumerable.Range(0,100).ToList();
            tasks = new List<Task>();
            results = new List<int>();
            errors = new List<int>();
        };

        Because of = () =>  values.ForEach(v => enqueue(v));

        It should_have_processed_all_the_tasks_in_sequence = async () => 
        {
            await when_ready();
            results.Any(v => v % 10 == 0).ShouldBeFalse();
        };
        It should_have_processed_all_errors = async () => 
        {
            await when_ready();
            errors.Any().ShouldBeTrue();
            errors.All(v => v % 10 == 0).ShouldBeTrue();
        };

        static void enqueue(int value)
        {
            tasks.Add(queue.Enqueue(() => {
                    if(value % 10 == 0)
                        throw new Exception($"Value is divisible by 10: {value}");
                    results.Add(value);
                    return Task.FromResult(value);
                },(t) => {
                    errors.Add(value);
                })
            );
        }  

        static async Task when_ready()
        {
            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            } 
            catch(Exception )
            {
                //nothing
            }
        } 
    }
}