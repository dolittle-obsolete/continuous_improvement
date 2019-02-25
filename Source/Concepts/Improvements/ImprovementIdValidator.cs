/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvements
{
    /// <summary>
    /// Validates an ImprovementId to make sure it is well formed
    /// </summary>
    public class ImprovementIdValidator : AbstractValidator<ImprovementId>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovementIdValidator" />
        /// </summary>
        public ImprovementIdValidator()
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
        /// Adds an ImprovementIdValidator and a Null Check to an ImprovementId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="ImprovementId" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, ImprovementId> MustBeAValidImprovementId<T>(this IRuleBuilder<T, ImprovementId> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An ImprovementId is required");
            return ruleBuilder.SetValidator(new ImprovementIdValidator());
		}
    }       
}
