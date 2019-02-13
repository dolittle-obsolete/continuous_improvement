/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Dolittle.ReadModels;

namespace Read.Improvables
{
    /// <summary>
    /// Represents a project
    /// </summary>
    public class ImprovableForListing : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="ImprovementConfigurationId"/> of the <see cref="Project"/>
        /// </summary>
        public ImprovableId Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current build version of the project
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the status for the improvable
        /// </summary>
        public ImprovableStatus Status { get; set; }
    }
}