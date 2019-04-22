/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts.SourceControl
{
    /// <summary>
    /// Validates a RepositoryFullName to make sure it is well formed
    /// </summary>
    public class RepositoryFullNameValidator : AbstractValidator<RepositoryFullName>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="RepositoryFullNameValidator" />
        /// </summary>
        public RepositoryFullNameValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .WithMessage("The RepositoryFullName cannot be empty");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds an RepositoryFullNameValidator and a Null Check to an RepositoryFullName
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <param name="isOptional">flag to indicate if the <see cref="RepositoryFullName" /> is optional on the command</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, RepositoryFullName> MustBeAValidRepositoryFullName<T>(this IRuleBuilder<T, RepositoryFullName> ruleBuilder, bool isOptional = false) 
        {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("An RepositoryFullName is required");
            return ruleBuilder.SetValidator(new RepositoryFullNameValidator());
		}
    }       
}
