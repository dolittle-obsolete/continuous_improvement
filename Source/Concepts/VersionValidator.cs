/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using FluentValidation;

namespace Concepts
{
    /// <summary>
    /// Indicates whether the version is in a valid format
    /// </summary>
    public class VersionValidator : AbstractValidator<Version>
    {
        /// <summary>
        /// Instantiates an instance of a <see cref="VersionValidator" />
        /// </summary>
        public VersionValidator()
        {
            RuleFor(_ => _.Value)
                .NotEmpty()
                .Matches(@"^(?<major>[0-9]+)\.(?<minor>[0-9]+)\.(?<patch>[0-9]+)(?:\-(?<build>[a-zA-Z0-9_]+))?$")
                .WithMessage("The version does not match the pattern [major].[minor].[patch]-[label]");
        }
    }

    /// <summary>
    /// Extensions to make it easier to include Concept validators in Input Validators
    /// </summary>
    public static partial class ValidatorBuilderExtensions 
    {
        /// <summary>
        /// Adds a VersionValidator and a Null Check to a Version
        /// </summary>
        /// <typeparam name="T">Type of the Command</typeparam>
        /// <param name="ruleBuilder">instance of the IRuleBuilder</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, Version> MustBeAValidVersion<T>(this IRuleBuilder<T, Version> ruleBuilder, bool isOptional = false) {
            if(!isOptional)
			    ruleBuilder.NotNull().WithMessage("A Version is required");
            return ruleBuilder.SetValidator(new VersionValidator());
		}       
    }
}
