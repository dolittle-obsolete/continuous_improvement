/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Improvables;
using Dolittle.ReadModels;

namespace Read.Improvables
{
    public class ImprovableConfiguration : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="ImprovementConfigurationId"/> of the <see cref="Project"/>
        /// </summary>
        public ImprovableId Id { get; set; }
        
        public string Name { get; set; }

    }
}