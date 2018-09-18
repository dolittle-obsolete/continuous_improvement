/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;

namespace Orchestrations
{
    /// <summary>
    /// Represents an implementation of <see cref="IConductor"/>
    /// </summary>
    public class Conductor : IConductor
    {
        /// <inheritdoc/>
        public void Conduct<T>(ScoreOf<T> score)
        {
            score.Steps.ForEach(_ => _.Perform(score.Context).Wait());
        }
    }
}