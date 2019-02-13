/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.ReadModels;

namespace Read.Improvements
{
    /// <summary>
    /// Represents an improvement
    /// </summary>
    public class Improvement : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="VersionString">version</see>
        /// </summary>
        public VersionString Version { get; set; }
    }
}