/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvables
{
    /// <summary>
    /// Validates an RecipeType to make sure it is well formed
    /// </summary>
    public class RecipeTypeValidator : AbstractValidator<RecipeType>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="RecipeTypeValidator" />
        /// </summary>
        public RecipeTypeValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The RecipeType cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an RecipeTypeValidator and a Null Check to an RecipeType
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="RecipeType" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, RecipeType> MustBeAValidRecipeType<T>(this IRuleBuilder<T, RecipeType> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("A RecipeType is required");
            return ruleBuilder.SetValidator(new RecipeTypeValidator());
		}
    }       
}
