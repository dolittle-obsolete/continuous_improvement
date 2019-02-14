/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Improvables
{
    public interface IRecipeManager
    {
        ExpandedRecipe Expand(Recipe recipe);
    }
}