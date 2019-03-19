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
    /// Records that an Installation was registered
    /// </summary>
    public class InstallationRegistered : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="InstallationRegistered" />
        /// </summary>
        /// <param name="installationId">The installation that was registered</param>
        /// <param name="targetType">The target type for this installation</param>
        /// <param name="targetAccount">The target account of this installation</param>
        /// <param name="repositories">The respositories associated with this installation</param>
        public InstallationRegistered(long installationId, string targetType, string targetAccount, IEnumerable<string> repositories)
        {
            InstallationId = installationId;
            TargetType = targetType;
            TargetAccount = targetAccount;
            Repositories = repositories;
        }
        /// <summary>
        /// The installation that was registered
        /// </summary>
        public long InstallationId { get; }
        /// <summary>
        /// The target type for this installation
        /// </summary>
        public string TargetType { get; }
        /// <summary>
        /// The target account of this installation
        /// </summary>
        public string TargetAccount { get; }
        /// <summary>
        /// The respositories associated with this installation
        /// </summary>
        public IEnumerable<string> Repositories { get; }
    }
}
