/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    /// <summary>
    /// Records that the Installation repositories were refreshed
    /// </summary>
    public class InstallationRepositoriesRefreshed : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="InstallationRepositoriesRefreshed" />
        /// </summary>
        /// <param name="installationId">The installation that was refreshed</param>
        /// <param name="repositories">The repositories associated with the installation</param>
        public InstallationRepositoriesRefreshed(long installationId, IEnumerable<string> repositories)
        {
            InstallationId = installationId;
            Repositories = repositories;
        }
        /// <summary>
        /// The installation that was refreshed
        /// </summary>
        public long InstallationId { get; }
        /// <summary>
        /// The repositories associated with the installation
        /// </summary>
        public IEnumerable<string> Repositories { get; }
    }
}
