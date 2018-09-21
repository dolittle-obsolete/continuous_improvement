/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Defines the conductor that conduct a <see cref="Score"/>
    /// </summary>
    public interface IConductor
    {
        /// <summary>
        /// Conduct a specific <see cref="Score"/>
        /// </summary>
        /// <param name="score"></param>
        void Conduct<T>(ScoreOf<T> score);
    }
}