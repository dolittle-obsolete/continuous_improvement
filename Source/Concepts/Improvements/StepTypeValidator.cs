/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an StepType to make sure it is well formed
    /// </summary>
    public class StepTypeValidator : AbstractValidator<StepType>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="StepTypeValidator" />
        /// </summary>
        public StepTypeValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The StepType cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an StepTypeValidator and a Null Check to an StepType
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="StepType" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, StepType> MustBeAValidStepType<T>(this IRuleBuilder<T, StepType> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An StepType is required");
            return ruleBuilder.SetValidator(new StepTypeValidator());
		}
    }       
}
