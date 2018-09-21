/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Defines the conductor that conduct a <see cref="ScoreOf{T}"/>
    /// </summary>
    public interface IConductor
    {
        /// <summary>
        /// Conduct a specific <see cref="ScoreOf{T}"/>
        /// </summary>
        /// <param name="score"><see cref="ScoreOf{T}">Score</see> to conduct</param>
        void Conduct<T>(ScoreOf<T> score);
    }
}