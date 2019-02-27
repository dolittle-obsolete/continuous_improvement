/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an StepNumber to make sure it is well formed
    /// </summary>
    public class StepNumberValidator : AbstractValidator<StepNumber>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="StepNumberValidator" />
        /// </summary>
        public StepNumberValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The StepNumber cannot be empty")
                .GreaterThan(0)
                .WithMessage("The StepNumber cannot be less than 1");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an StepNumberValidator and a Null Check to an StepNumber
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="StepNumber" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, StepNumber> MustBeAValidStepNumber<T>(this IRuleBuilder<T, StepNumber> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An StepNumber is required");
            return ruleBuilder.SetValidator(new StepNumberValidator());
		}
    }       
}
