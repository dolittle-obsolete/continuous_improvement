/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Concepts.Improvables;
using Dolittle.Types;

namespace Policies.Improvements
{
    /// <inheritdoc />
    public class RecipeLocator : IRecipeLocator
    {
        readonly ITypeFinder _typeFinder;
        /// <summary>
        /// Instantiates an instance of <see cref="RecipeLocator" />
        /// </summary>
        /// <param name="typeFinder"></param>
        public RecipeLocator(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <inheritdoc />
        public IRecipe GetByType(RecipeType name)
        {
            var typeName = $"{typeof(RecipeLocator).Namespace}.Recipes.{name}";
            var type = _typeFinder.FindTypeByFullName(typeName);
            var recipe = Activator.CreateInstance(type) as IRecipe;
            return recipe;
        }
    }
}