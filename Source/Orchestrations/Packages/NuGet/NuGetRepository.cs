/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Orchestrations.Packages.NuGet
{
    /// <summary>
    /// Represents a repository for NuGet packages
    /// </summary>
    public class NuGetRepository
    {
        /// <summary>
        /// Gets or sets the URL for the source
        /// </summary>
        public string Source;

        /// <summary>
        /// Gets or sets the ApiKey to use
        /// </summary>
        public string ApiKey;

        /// <summary>
        /// Gets or sets the condition for pushing
        /// </summary>
        public Condition   CanPush = (c) => true;
    }
}