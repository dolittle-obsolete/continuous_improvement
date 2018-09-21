/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Logging;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents an implementation of <see cref="IConductor"/>
    /// </summary>
    public class Conductor : IConductor
    {
        readonly IContainer _container;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Conductor"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to manage instances</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Conductor(IContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Conduct<T>(ScoreOf<T> score)
        {
            score.Steps.ForEach(_ =>
            {
                _logger.Information($"Performer {_.Name}");
                var performer = _container.Get(_);
                var canPerform = _.GetMethod("CanPerform");
                var perform = _.GetMethod("Perform");
                var context = score.Context;

                _logger.Information($"Checking if performer can perform");
                if ((bool) canPerform.Invoke(performer, new object[] { context }))
                {
                    _logger.Information("Perfoming");
                    var task = perform.Invoke(performer, new object[] { context }) as Task;
                    task.Wait();
                    _logger.Information("Performed");
                }
            });
        }
    }
}