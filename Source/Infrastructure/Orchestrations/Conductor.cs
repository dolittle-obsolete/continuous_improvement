/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Reflection;

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
            score.Steps.ForEach(step =>
            {
                _logger.Information($"Performer {step.Type.Name}");
                var performer = _container.Get(step.Type);
                SetConfigurationForPerformer(step, performer);
                PerformIfCanPerform(score.Context, step, performer);
            });
        }

        void PerformIfCanPerform<T>(T context, Step step, object performer)
        {
            var canPerform = step.Type.GetMethod("CanPerform");
            var perform = step.Type.GetMethod("Perform");
            _logger.Information($"Checking if performer can perform");
            if ((bool)canPerform.Invoke(performer, new object[] { context }))
            {
                _logger.Information("Perfoming");
                var task = perform.Invoke(performer, new object[] { context }) as Task;
                task.Wait();
                _logger.Information("Performed");
            }
        }

        void SetConfigurationForPerformer(Step step, object performer)
        {
            if (step.Configuration != null && step.Type.HasInterface(typeof(INeedConfigurationOf<>)))
            {
                var configProperty = step.Type.GetProperty("Config", BindingFlags.Public | BindingFlags.Instance);
                configProperty.SetValue(performer, step.Configuration);
            }
        }
    }
}