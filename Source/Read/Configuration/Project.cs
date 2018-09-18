/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts;

namespace Read.Configuration
{
    /// <summary>
    /// Represents a project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the <see cref="ProjectId"/>
        /// </summary>
        public ProjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ProjectName"/>
        /// </summary>
        public ProjectName Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Uri"/> for the repository to use
        /// </summary>
        public Uri Repository { get; set; }

        /// <summary>
        /// Gets or sets the type of project
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="ProjectCascade">projects</see> to cascade to
        /// </summary>
        public IEnumerable<ProjectCascade> Cascades { get; set; } = new ProjectCascade[0];
    }
}