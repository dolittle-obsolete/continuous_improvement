/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Events;

namespace Events.SourceControl.GitHub
{
    /// <summary>
    /// Records that an installation was unregistered.
    /// </summary>
    public class InstallationUnregistered : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="InstallationUnregistered" />
        /// </summary>
        /// <param name="installationId"></param>
        /// <param name="repositories"></param>
        public InstallationUnregistered(long installationId, IEnumerable<string> repositories)
        {
            InstallationId = installationId;
            Repositories = repositories;
        }
        /// <summary>
        /// The installation that was unregistered
        /// </summary>
        public long InstallationId { get; }
        /// <summary>
        /// The repositories that were unregistered
        /// TODO: I don't think this should be here. The processor can find the repositories associated with this
        /// installation and remove them if we have them stored in that fashion.
        /// </summary>
        public IEnumerable<string> Repositories { get; }
    }
}
