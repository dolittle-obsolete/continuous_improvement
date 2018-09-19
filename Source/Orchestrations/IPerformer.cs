/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;

namespace Orchestrations
{
    /// <summary>
    /// Defines a performer in an orchestra
    /// </summary>
    public interface IPerformer<T>
    {
        /// <summary>
        /// The method gets called to check if the performer can perform
        /// </summary>
        /// <param name="score">The score</param>
        /// <returns>True if it can perform, false if not</returns>
        bool CanPerform(T score);

        /// <summary>
        /// The method that gets called when the performer should perform
        /// </summary>
        /// <param name="score">The score</param>
        Task Perform(T score);
    }
}