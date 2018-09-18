/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Read.Configuration
{
    /// <summary>
    /// Represents the configuration of a NuGet server for publishing packages to
    /// </summary>
    public class NuGetPackageServerConfiguration 
    {
        /// <summary>
        /// Gets or sets the <see cref="Uri"/> for the server
        /// </summary>
        public Uri Server { get; set; }

        /// <summary>
        /// Gets or sets the access key to use
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Get whether or not the server configuration is enabled or not
        /// </summary>
        public bool IsEnabled => Server != null && !string.IsNullOrEmpty(Key);
    }
}