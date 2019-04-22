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
    /// A <see cref="ICommand">command</see> to instruct an unregistering of an <see cref="Installation" />
    /// </summary>
    public class UnregisterInstallation : ICommand
    {
        /// <summary>
        /// The Id of the <see cref="Installation" /> to unregister
        /// </summary>
        public InstallationId Id { get; set; }
    }
}
