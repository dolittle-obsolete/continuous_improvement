/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Improvables
{
    /// <summary>
    /// Defines a recipe manager
    /// </summary>
    public interface IRecipeManager
    {
        /// <summary>
        /// Expands a recipe by adding additional information
        /// </summary>
        /// <param name="recipe">The recipe to expand</param>
        ExpandedRecipe Expand(Recipe recipe);
    }
}