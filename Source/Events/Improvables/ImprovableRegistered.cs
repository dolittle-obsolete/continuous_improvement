/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Events;

namespace Events.Improvables
{
    /// <summary>
    /// Records that an Improvable was registered
    /// </summary>
    public class ImprovableRegistered : IEvent
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ImprovableRegistered" />
        /// </summary>
        /// <param name="improvable">The improvable being registered</param>
        /// <param name="name">The name of the improvable</param>
        /// <param name="recipe">The recipe type associated with the improvable</param>
        /// <param name="repository">The source code repository for this improvable</param>
        /// <param name="path">The path within the source control repository to the building artifact
        /// </param>
        public ImprovableRegistered(Guid improvable, string name, string recipe, string repository, string path)
        {
            this.Improvable = improvable;
            this.Name = name;
            this.Recipe = recipe;
            this.Repository = repository;
            this.Path = path;

        }

        /// <summary>
        /// The <see cref="Improvable" /> being registered
        /// </summary>
        public Guid Improvable { get; set; }
        /// <summary>
        /// The string name for the <see cref="Improvable" />
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The recipe type to associate with this <see cref="Improvable" />
        /// </summary>
        public string Recipe { get; set; }
        /// <summary>
        /// The source control repository associated with this <see cref="Improvable" />
        /// </summary>
        public string Repository { get; set; }
        /// <summary>
        /// The path within the Source Control repository to the building artifact
        /// </summary>
        public string Path { get; set; }

    }
}