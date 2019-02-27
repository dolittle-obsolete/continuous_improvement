/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Frameworks
{
    /// <summary>
    /// Validates an FrameworkId to make sure it is well formed
    /// </summary>
    public class FrameworkIdValidator : AbstractValidator<FrameworkId>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="FrameworkIdValidator" />
        /// </summary>
        public FrameworkIdValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The Id cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an FrameworkIdValidator and a Null Check to an FrameworkId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="FrameworkId" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, FrameworkId> MustBeAValidFrameworkId<T>(this IRuleBuilder<T, FrameworkId> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("A FrameworkId is required");
            return ruleBuilder.SetValidator(new FrameworkIdValidator());
		}
    }       
}
