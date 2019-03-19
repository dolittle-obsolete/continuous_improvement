/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    /// <summary>
    /// Records that the repositories for an installation were updated
    /// </summary>
    public class InstallationRepositoriesUpdateReceived : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="InstallationRepositoriesUpdateReceived" />
        /// </summary>
        /// <param name="installationId">The installation that was updated</param>
        /// <param name="repositoriesAdded">The repositories that were added</param>
        /// <param name="repositoriesRemoved">The repositories that were removed</param>
        public InstallationRepositoriesUpdateReceived(long installationId, IEnumerable<string> repositoriesAdded, IEnumerable<string> repositoriesRemoved)
        {
            InstallationId = installationId;
            RepositoriesAdded = repositoriesAdded;
            RepositoriesRemoved = repositoriesRemoved;
        }
        /// <summary>
        /// The installation that was updated
        /// </summary>
        public long InstallationId { get; }
        /// <summary>
        /// The repositories that were added
        /// </summary>
        public IEnumerable<string> RepositoriesAdded { get; }
        /// <summary>
        /// The repositories that were removed
        /// </summary>
        public IEnumerable<string> RepositoriesRemoved { get; }
    }
}
