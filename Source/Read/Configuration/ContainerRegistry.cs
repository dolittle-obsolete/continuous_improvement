/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Configuration
{
    /// <summary>
    /// Represents a container registry - a place to publish any container images
    /// </summary>
    public class ContainerRegistry
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="ContainerRegistry"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username for the <see cref="ContainerRegistry"/>
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the <see cref="ContainerRegistry"/>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the condition the <see cref="ContainerRegistry"/> will be used
        /// </summary>
        public string Condition { get; set; }
    }
}