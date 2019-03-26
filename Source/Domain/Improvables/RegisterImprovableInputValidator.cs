/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;
using Concepts.Improvables;
using Concepts.SourceControl;

namespace Domain.Improvables
{
    /// <summary>
    /// Validates that a <see cref="RegisterImprovable" /> command is well formed.
    /// </summary>
    public class RegisterImprovableInputValidator : CommandInputValidatorFor<RegisterImprovable>
    {
        /// <summary>
        /// Instantiates an instance of <see cref="RegisterImprovableInputValidator" />
        /// </summary>
        public RegisterImprovableInputValidator()
        {
            RuleFor(_ => _.Improvable)
                .MustBeAValidImprovableId();
            RuleFor(_ => _.Name)
                .MustBeAValidImprovableName();
            RuleFor(_ => _.Recipe)
                .MustBeAValidRecipeType();
            RuleFor(_ => _.Repository)
                .MustBeAValidRepositoryFullName();
        }
    }
}
