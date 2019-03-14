/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// A queue for <see cref="Task">Tasks</see> that will be performed sequentially, where each <see cref="Task" />
    /// will await the previous task.static  Errors can be handled explicitly and do not
    /// prevent the next <see cref="Task" /> executing. 
    /// </summary>
    public class TaskQueue
    {
        private readonly object _lock = new object();
        WeakReference<TaskAndErrorHandler> _previous;

        /// <summary>
        /// Enqueues a task to be executed in sequence. With an optional error handler.
        /// </summary>
        /// <param name="action">Task to be peformed</param>
        /// <param name="onError">Error Handler</param>
        /// <returns>A Task</returns>
        public virtual Task Enqueue(Func<Task> action, Action<Task> onError = null)
        {
            lock(_lock)
            {
                TaskAndErrorHandler previousTask;
                Task newTask;

                if (_previous == null || !_previous.TryGetTarget(out previousTask))
                {
                    previousTask = new TaskAndErrorHandler(Task.CompletedTask,onError);
                }

                newTask = Task.Run(async() =>
                {
                    try
                    {
                        await previousTask.Task;
                    }
                    catch (Exception ex)
                    {
                        previousTask.ErrorHandler(Task.FromException(ex));
                    }
                    await action();
                });

                _previous = new WeakReference<TaskAndErrorHandler>(new TaskAndErrorHandler(newTask,onError));
                return newTask;
            }
        }

        // public virtual Task<T> Enqueue<T>(Func<T> action, Action<Task> onError = null);
        // public virtual Task Enqueue(Action action, Action<Task> onError = null);

        //We need to hold the correct error handler to fire off when the task fails.
        private class TaskAndErrorHandler
        {
            public TaskAndErrorHandler(Task task, Action<Task> errorHandler)
            {
                Task = task;
                ErrorHandler = errorHandler;
            }

            public Task Task { get; }
            public Action<Task> ErrorHandler { get; }
        }
    }
}