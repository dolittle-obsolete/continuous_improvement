/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Orchestrations.Packages.NuGet
{
    /// <summary>
    /// Represents the configuration for NuGet
    /// </summary>
    public class NuGetConfig
    {
        /// <summary>
        /// Gets or sets the repositories to push to
        /// </summary>
        public IEnumerable<NuGetRepository> Repositories = new NuGetRepository[0];
    }
}