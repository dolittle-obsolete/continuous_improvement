/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.ReadModels;

namespace Read.Improvements
{
    /// <summary>
    /// Represents the raw log output from a step
    /// </summary>
    public class StepRawLog : IReadModel
    {
        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }
    }
}