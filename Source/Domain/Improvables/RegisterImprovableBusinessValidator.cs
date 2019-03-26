/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;
using Dolittle.Commands.Validation;
using FluentValidation;

namespace Domain.Improvables
{
    /// <summary>
    /// Validates that business rules associated with a <see cref="RegisterImprovable" /> command are satisfied
    /// </summary>
    public class RegisterImprovableBusinessValidator : CommandBusinessValidatorFor<RegisterImprovable>
    {
        private readonly ImprovableExists _improvableExists;

        /// <summary>
        /// Validates the business rules associated with registering a new <see cref="Improvable" />
        /// </summary>
        /// <param name="improvableExists"></param>
        public RegisterImprovableBusinessValidator(ImprovableExists improvableExists)
        {
            _improvableExists = improvableExists;

            RuleFor(_ => _.Improvable)
                .Must(NotAlreadyBeRegistered)
                .WithMessage("Cannot register an improvable that is already registered.");
            
        }

        bool NotAlreadyBeRegistered(ImprovableId improvable)
        {
            return !_improvableExists(improvable);
        }
    }
}
