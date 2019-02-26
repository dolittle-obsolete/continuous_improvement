/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an StepStatus to make sure it is well formed
    /// </summary>
    public class StepStatusValidator : AbstractValidator<StepStatus>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="StepStatusValidator" />
        /// </summary>
        public StepStatusValidator()
        {
            RuleFor(_ => _)
                .NotEmpty()
                .WithMessage("The StepStatus cannot be empty")
                .IsInEnum().WithMessage(_ => $"'_' is not a valid value for StepStatus");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an StepStatusValidator and a Null Check to an ImprovableId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="StepStatus" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, StepStatus> MustBeAValidStepStatus<T>(this IRuleBuilder<T, StepStatus> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An StepStatus is required");
            return ruleBuilder.SetValidator(new StepStatusValidator());
		}
    }  
}
