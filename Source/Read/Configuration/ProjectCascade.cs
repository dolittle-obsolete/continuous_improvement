/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;

namespace Read.Configuration
{
    /// <summary>
    /// Represents a project to cascade for build
    /// </summary>
    public class ProjectCascade
    {
        /// <summary>
        /// Gets or sets the <see cref="ProjectId"/>
        /// </summary>
        public ProjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ProjectName"/>
        /// </summary>
        public ProjectName Name { get; set; }
    }
}