/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.ReadModels;

namespace Read.Improvements
{
    /// <summary>
    /// Represents a project
    /// </summary>
    public class Project : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="ProjectId"/> of the <see cref="Project"/>
        /// </summary>
        public ProjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current build version of the project
        /// </summary>
        public string Version { get; set; }
    }
}