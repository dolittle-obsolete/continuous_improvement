/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Concepts.Improvables;
using Concepts.SourceControl;
using Dolittle.Commands.Validation;
using FluentValidation;

namespace Domain.Improvables
{
    /// <summary>
    /// Validates that business rules associated with a <see cref="RegisterImprovable" /> command are satisfied
    /// </summary>
    public class RegisterImprovableBusinessValidator : CommandBusinessValidatorFor<RegisterImprovable>
    {
        private readonly ImprovableExists _improvableExists;
        private readonly ImprovableNameExists _improvableNameExists;
        private readonly RecipeTypeExists _recipeExists;
        private readonly RepositoryExists _repoExists;

        /// <summary>
        /// Validates the business rules associated with registering a new <see cref="Improvable" />
        /// </summary>
        /// <param name="improvableExists">Indicates if an Improvable with the Id exists</param>
        /// <param name="improvableNameExists">Indicates if an Improvable with the name already exists</param>
        /// <param name="recipeExists">Indicates if the recipe type exists</param>
        /// <param name="repoExists"></param>
        public RegisterImprovableBusinessValidator(ImprovableExists improvableExists, ImprovableNameExists improvableNameExists, RecipeTypeExists recipeExists, RepositoryExists repoExists)
        {
            _improvableExists = improvableExists;
            _improvableNameExists = improvableNameExists;
            _recipeExists = recipeExists;
            _repoExists = repoExists;

            RuleFor(_ => _.Improvable)
                .Must(NotAlreadyBeRegistered)
                .WithMessage(_ => $"Cannot register an improvable ('{_.Improvable.Value}') that is already registered.");
            RuleFor(_ => _.Name)
                .Must(NotAlreadyBeInUse)
                .WithMessage(_ => $"An improvable with the name '{_.Name.Value}' is already registered.");
            RuleFor(_ => _.Recipe)
                .Must(BeAnExistingRecipe)
                .WithMessage(_ => $"No recipe for of type '{_.Recipe.Value}' exists.");
            RuleFor(_ => _.Repository)
                .Must(BeAnExistingRepository)
                .WithMessage(_ => $"No repository with this identifier '{_.Repository.Value}' exists.");    
            
        }

        bool BeAnExistingRepository(RepositoryFullName repo)
        {
            return _repoExists(repo);
        }

        bool BeAnExistingRecipe(RecipeType recipe)
        {
            return _recipeExists(recipe);
        }

        bool NotAlreadyBeRegistered(ImprovableId improvable)
        {
            return !_improvableExists(improvable);
        }

        bool NotAlreadyBeInUse(ImprovableName name)
        {
            return !_improvableNameExists(name);
        }
    }
}
