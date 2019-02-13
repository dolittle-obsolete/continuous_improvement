/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Improvables
{
    public class RecipeManager : IRecipeManager
    {
        public ExpandedRecipe Expand(Recipe recipe)
        {
            var expandedRecipe = new ExpandedRecipe
            {
                Type = recipe.Type,
                Package = recipe.Package,
                Publish = recipe.Publish,
                BasePath = recipe.BasePath

            };

            return expandedRecipe;
        }
    }
}