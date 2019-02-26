/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvables
{
    /// <summary>
    /// Validates an ImprovableStatus to make sure it is well formed
    /// </summary>
    public class ImprovableStatusValidator : AbstractValidator<ImprovableStatus>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovableStatusValidator" />
        /// </summary>
        public ImprovableStatusValidator()
        {
            RuleFor(_ => _)
                .NotEmpty()
                .WithMessage("The Status cannot be empty")
                .IsInEnum().WithMessage(_ => $"'_' is not a valid value for Improvable Status");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an ImprovableStatusValidator and a Null Check to an ImprovableId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="ImprovableStatus" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, ImprovableStatus> MustBeAValidImprovableStatus<T>(this IRuleBuilder<T, ImprovableStatus> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An ImprovableStatus is required");
            return ruleBuilder.SetValidator(new ImprovableStatusValidator());
		}
    }       
}
