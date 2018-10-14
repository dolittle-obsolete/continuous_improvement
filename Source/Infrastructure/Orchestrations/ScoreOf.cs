/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Reflection;

namespace Infrastructure.Orchestrations
{

    /// <summary>
    /// Represents a score that can be performed and should be <see cref="IConductor">conducted</see>
    /// </summary>
    public class ScoreOf<T> where T:BaseContext
    {
        readonly List<Step> _steps = new List<Step>();

        /// <summary>
        /// Initializes a new instance of <see cref="ScoreOf{T}"/>
        /// </summary>
        /// <param name="context">Context for the score</param>
        public ScoreOf(T context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the context for the score
        /// </summary>
        public T Context { get; }

        /// <summary>
        /// Gets all the steps for the score
        /// </summary>
        public IEnumerable<Step> Steps => _steps;

        /// <summary>
        /// Add a step to the score
        /// </summary>
        /// <typeparam name="TPerformer">Type of performer</typeparam>
        public void AddStep<TPerformer>() where TPerformer:IPerformer<T>
        {
            ThrowIfPerformerNeedsConfiguration<TPerformer>();
            var number = _steps.Count()+1;
            _steps.Add(new Step(typeof(TPerformer), number));
        }

        /// <summary>
        /// Add a step to the score
        /// </summary>
        /// <typeparam name="TPerformer">Type of performer</typeparam>
        /// <typeparam name="TConfig">Type of configuration object expected</typeparam>
        public void AddStep<TPerformer, TConfig>(TConfig configuration) where TPerformer:IPerformer<T>
        {
            ThrowIfPerformerDosNotNeedConfiguration<TPerformer>();
            var number = _steps.Count()+1;
            _steps.Add(new Step(typeof(TPerformer), number, configuration));
        }

        void ThrowIfPerformerNeedsConfiguration<TPerformer>() where TPerformer : IPerformer<T>
        {
            if( typeof(TPerformer).HasInterface(typeof(INeedConfigurationOf<>))) throw new PerformerNeedsConfiguration(typeof(TPerformer));
        }

        void ThrowIfPerformerDosNotNeedConfiguration<TPerformer>() where TPerformer : IPerformer<T>
        {
            if( !typeof(TPerformer).HasInterface(typeof(INeedConfigurationOf<>))) throw new PerformerDoesNotNeedConfiguration(typeof(TPerformer));
        }
    }
}