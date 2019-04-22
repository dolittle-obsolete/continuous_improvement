/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl;
using Concepts.SourceControl.GitHub;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    /// <summary>
    /// A list of all repositories for an installation
    /// </summary>
    public class InstallationRepositories : IReadModel
    {
        /// <summary>
        /// The Id of the installation
        /// </summary>
        public InstallationId Id { get; set; }
        /// <summary>
        /// A list of all repositories by name
        /// </summary>
        public IList<RepositoryFullName> Repositories { get; set; }
    }
}
