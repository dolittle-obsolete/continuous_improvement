/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an ImprovementType to make sure it is well formed
    /// </summary>
    public class ImprovementTypeValidator : AbstractValidator<ImprovementType>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovementTypeValidator" />
        /// </summary>
        public ImprovementTypeValidator()
        {
            RuleFor(_ => _)
                .NotEmpty()
                .WithMessage("The Improvment Type cannot be empty")
                .IsInEnum().WithMessage(_ => $"'_' is not a valid value for Improvement Type");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an ImprovementTypeValidator and a Null Check to an ImprovableId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="ImprovementType" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, ImprovementType> MustBeAValidImprovementType<T>(this IRuleBuilder<T, ImprovementType> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An ImprovementType is required");
            return ruleBuilder.SetValidator(new ImprovementTypeValidator());
		}
    }       
}
