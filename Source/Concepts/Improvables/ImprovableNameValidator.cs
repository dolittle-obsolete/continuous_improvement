/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvables
{
    /// <summary>
    /// Validates an ImprovableName to make sure it is well formed
    /// </summary>
    public class ImprovableNameValidator : AbstractValidator<ImprovableName>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovableNameValidator" />
        /// </summary>
        public ImprovableNameValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The ImprovableName cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an ImprovableNameValidator and a Null Check to an ImprovableName
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="ImprovableName" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, ImprovableName> MustBeAValidImprovableName<T>(this IRuleBuilder<T, ImprovableName> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An ImprovableName is required");
            return ruleBuilder.SetValidator(new ImprovableNameValidator());
		}
    }       
}
