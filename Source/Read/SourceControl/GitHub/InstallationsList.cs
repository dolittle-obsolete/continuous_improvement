/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    /// <summary>
    /// A list of all installations
    /// </summary>
    public class InstallationsList : IReadModel
    {
        /// <summary>
        /// Default Id as this is a global list
        /// </summary>
        public int Id { get; } = 0;
        /// <summary>
        /// A list of all installations by Id
        /// </summary>
        public IList<InstallationId> Installations { get; set; }
    }
}
