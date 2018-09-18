/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Configuration
{
    /// <summary>
    /// Represents the configuration of NuGet packages
    /// </summary>
    public class NuGetPackages
    {
        /// <summary>
        /// Gets or sets the release server configuration for publishing packages to
        /// </summary>
        public NuGetPackageServerConfiguration Release { get; set; } = new NuGetPackageServerConfiguration();

        /// <summary>
        /// Gets or sets the pre-release server configuration for publishing packages to
        /// </summary>
        public NuGetPackageServerConfiguration PreRelease { get; set; } = new NuGetPackageServerConfiguration();
    }
}