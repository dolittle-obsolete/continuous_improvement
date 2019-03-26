/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// A <see cref="ICommand">command</see> to instruct the updating of the repositories associated with an <see cref="Installation" />
    /// </summary>
    public class UpdateInstallationRepositories : ICommand
    {
        /// <summary>
        /// The id of the <see cref="Installation" /> being updated
        /// </summary>
        public InstallationId Id { get; set; }
        /// <summary>
        /// A list of repositories to be added
        /// </summary>
        public IEnumerable<RepositoryFullName> RepositoriesAdded { get; set; }
        /// <summary>
        /// A list of repositories to be removed
        /// </summary>
        public IEnumerable<RepositoryFullName> RepositoriesRemoved { get; set; }
    }
}
