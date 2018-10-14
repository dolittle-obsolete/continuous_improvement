/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents a log message for a <see cref="IPerformer{T}"/>
    /// </summary>
    public class PerformerLogMessage
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTimeOffset"/> for when it happened
        /// </summary>
        public DateTimeOffset Time {  get; set; }

        /// <summary>
        /// Gets or sets the actual message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the source
        /// </summary>
        public string Source {  get; set; }

        /// <summary>
        /// Gets or sets the line from the source
        /// </summary>
        public int Line {  get; set; }
    }

}