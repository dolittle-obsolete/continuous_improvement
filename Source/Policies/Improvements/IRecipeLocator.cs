/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Improvables;

namespace Policies.Improvements
{
    public interface IRecipeLocator
    {
        IRecipe GetByName(RecipeType name);
    }
}