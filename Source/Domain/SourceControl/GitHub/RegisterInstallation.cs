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
    /// A command to register an installation
    /// </summary>
    public class RegisterInstallation : ICommand
    {
        /// <summary>
        /// The Id of the installation being registered
        /// </summary>
        public InstallationId Id { get; set; }
    }
}
