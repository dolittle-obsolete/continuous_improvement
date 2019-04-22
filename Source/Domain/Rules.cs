/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;
using Concepts.SourceControl;

namespace Domain
{
    /// <summary>
    /// Indicates whether or not there is an <see cref="ImprovableId" /> with the specified Id
    /// </summary>
    /// <param name="id">The Id of the improvable</param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool ImprovableExists(ImprovableId id);

    /// <summary>
    /// Indicates whether or not there is a <see cref="RecipeType">recipe</see> with this id.
    /// </summary>
    /// <param name="recipe">The string identifier of the <see cref="RecipeType" /></param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool RecipeTypeExists(RecipeType recipe);    

    /// <summary>
    /// Indicates whether or not there is a <see cref="RepositoryFullName">repository with this id</see>.
    /// </summary>
    /// <param name="repo">The string identifier of the <see cref="RepositoryFullName">repository</see></param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool RepositoryExists(RepositoryFullName repo);   
}