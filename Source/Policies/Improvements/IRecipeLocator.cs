/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Improvables;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a <see cref="IRecipe">recipe</see> locator
    /// </summary>
    public interface IRecipeLocator
    {
        /// <summary>
        /// Gets a Recipe by the <see cref="RecipeType" />
        /// </summary>
        /// <param name="name">The <see cref="RecipeType" /> to get</param>
        /// <returns>A <see cref="IRecipe">recipe</see></returns>
        IRecipe GetByType(RecipeType name);
    }
}