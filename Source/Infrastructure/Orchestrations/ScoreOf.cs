/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents a score that can be performed and should be <see cref="IConductor">conducted</see>
    /// </summary>
    public class ScoreOf<T>
    {
        readonly List<Type> _steps = new List<Type>();

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
        public IEnumerable<Type> Steps => _steps;

        /// <summary>
        /// Add a step to the score
        /// </summary>
        /// <typeparam name="TPerformer">Type of performer</typeparam>
        public void AddStep<TPerformer>() where TPerformer:IPerformer<T>
        {
            _steps.Add(typeof(TPerformer));
        }
    }
}