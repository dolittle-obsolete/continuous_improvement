/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Concepts.Improvables;
using Dolittle.Types;

namespace Policies.Improvements
{
    public class RecipeLocator : IRecipeLocator
    {
        readonly ITypeFinder _typeFinder;

        public RecipeLocator(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public IRecipe GetByName(RecipeType name)
        {
            var typeName = $"{typeof(RecipeLocator).Namespace}.Recipes.{name}";
            var type = _typeFinder.FindTypeByFullName(typeName);
            var recipe = Activator.CreateInstance(type) as IRecipe;
            return recipe;
        }
    }
}