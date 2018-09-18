/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Orchestrations
{
    /// <summary>
    /// Represents a score that can be performed and should be <see cref="IConductor">conducted</see>
    /// </summary>
    public class ScoreOf<T>
    {
        readonly List<IPerformer<T>> _steps = new List<IPerformer<T>>();

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
        public IEnumerable<IPerformer<T>> Steps => _steps;

        /// <summary>
        /// Add a step to the score
        /// </summary>
        /// <param name="performer"></param>
        public void AddStep(IPerformer<T> performer)
        {
            _steps.Add(performer);
        }
    }
}