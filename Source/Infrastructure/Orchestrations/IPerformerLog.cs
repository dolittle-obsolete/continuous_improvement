/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Defines a log for a <see cref="IPerformer{T}"/>
    /// </summary>
    public interface IPerformerLog
    {
        /// <summary>
        /// Log information to the log
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="source">Source it is coming from - if known - default = empty string</param>
        /// <param name="line">Line it is coming from - if known - default = 0</param>
        void Information(string message, string source="", int line=0);

        /// <summary>
        /// Log an error to the log
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="source">Source it is coming from - if known - default = empty string</param>
        /// <param name="line">Line it is coming from - if known - default = 0</param>
        void Error(string message, string source="", int line=0);

        /// <summary>
        /// Log warning to the log
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="source">Source it is coming from - if known - default = empty string</param>
        /// <param name="line">Line it is coming from - if known - default = 0</param>
        void Warning(string message, string source="", int line=0);
    }
}