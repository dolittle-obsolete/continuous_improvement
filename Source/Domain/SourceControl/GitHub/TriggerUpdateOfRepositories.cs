/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// A <see cref="ICommand" /> to trigger the update of the repositories
    /// </summary>
    public class TriggerUpdateOfRepositories : ICommand
    {
        /// <summary>
        /// The <see cref="Installation">installations</see> to trigger an update for.
        /// </summary>
        public IEnumerable<InstallationId> InstallationIds { get; set; }
    }
}
