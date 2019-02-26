/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.Improvables
{
    /// <summary>
    /// Validates an ImprovableId to make sure it is well formed
    /// </summary>
    public class ImprovableIdValidator : AbstractValidator<ImprovableId>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovableIdValidator" />
        /// </summary>
        public ImprovableIdValidator()
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
        /// Adds an ImprovableIdValidator and a Null Check to an ImprovableId
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="ImprovableId" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, ImprovableId> MustBeAValidImprovableId<T>(this IRuleBuilder<T, ImprovableId> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("A ImprovableId is required");
            return ruleBuilder.SetValidator(new ImprovableIdValidator());
		}
    }       
}
