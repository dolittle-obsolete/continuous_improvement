/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an StepResultSeverity to make sure it is well formed
    /// </summary>
    public class StepResultSeverityValidator : AbstractValidator<StepResultSeverity>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="StepResultSeverityValidator" />
        /// </summary>
        public StepResultSeverityValidator()
        {
            RuleFor(_ => _)
                .NotEmpty()
                .WithMessage("The StepResultSeverity cannot be empty")
                .IsInEnum().WithMessage(_ => $"'_' is not a valid value for StepResultSeverity");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an StepResultSeverityValidator and a Null Check to an ImprovableId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="StepResultSeverity" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, StepResultSeverity> MustBeAValidStepResultSeverity<T>(this IRuleBuilder<T, StepResultSeverity> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An StepResultSeverity is required");
            return ruleBuilder.SetValidator(new StepResultSeverityValidator());
		}
    }       
}
