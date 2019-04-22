/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts;
using Concepts.Improvements;
using Concepts.Improvables;
using Dolittle.Commands.Validation;
using FluentValidation;
namespace Domain.Improvements
{
    /// <summary>
    /// Input Validator for the <see cref="InitiateImprovement" /> command
    /// </summary>
    public class InitiateImprovementInputValidator : CommandInputValidatorFor<InitiateImprovement>
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InitiateImprovementInputValidator" />
        /// </summary>
        public InitiateImprovementInputValidator()
        {
            RuleFor(_ => _.Improvement)
                .MustBeAValidImprovementId();
            RuleFor(_ => _.ForImprovable)
                .MustBeAValidImprovableId();
            RuleFor(_ => _.Version)
                .MustBeAValidVersion();
        }
    }
}
