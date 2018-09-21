/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.DependencyInversion;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents an implementation of <see cref="IConductor"/>
    /// </summary>
    public class Conductor : IConductor
    {
        readonly IContainer _container;

        public Conductor(IContainer container)
        {
            _container = container;
        }

        /// <inheritdoc/>
        public void Conduct<T>(ScoreOf<T> score)
        {
            score.Steps.ForEach(_ =>
            {
                var performer = _container.Get(_);
                var canPerform = _.GetMethod("CanPerform");
                var perform = _.GetMethod("Perform");
                var context = score.Context;
                if ((bool) canPerform.Invoke(performer, new object[] { context }))
                {
                    var task = perform.Invoke(performer, new object[] { context }) as Task;
                    task.Wait();
                }
            });
        }
    }
}