/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents the continuous improvement context 
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>
        /// </summary>
        /// <param name="project"><see cref="Project"/> configuration</param>
        public Context(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Gets the <see cref="Project"/> configuration object
        /// </summary>
        public Project Project { get; }
    }
}