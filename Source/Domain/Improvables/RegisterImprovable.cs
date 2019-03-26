/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;
using Concepts.SourceControl;
using Dolittle.Commands;

namespace Domain.Improvables
{
    /// <summary>
    /// An instruction to register an improvable
    /// </summary>
    public class RegisterImprovable : ICommand
    {
        /// <summary>
        /// The <see cref="Improvable" /> being registered
        /// </summary>
        public ImprovableId Improvable { get; set; }
        /// <summary>
        /// The string name for the <see cref="Improvable" />
        /// </summary>
        public ImprovableName Name { get; set; }
        /// <summary>
        /// The <see cref="RecipeType">recipe type</see> to associate with this <see cref="Improvable" />
        /// </summary>
        public RecipeType Recipe { get; set; }
        /// <summary>
        /// The source control repository associated with this <see cref="Improvable" />
        /// </summary>
        public RepositoryFullName Repository { get; set; } 
        /// <summary>
        /// The path within the Source Control repository to the building artifact
        /// </summary>
        public Path Path { get; set;}
    }
}
