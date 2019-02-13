/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/


namespace Read.Configuration
{
    /// <summary>
    /// Represents a package source - a place to publish any packages
    /// </summary>
    public class Deployment
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="Deployment"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="Deployment"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the authorization key to use for the <see cref="Deployment"/>
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the condition the <see cref="Deployment"/> will be used
        /// </summary>
        public string Condition { get; set; }
    }

}