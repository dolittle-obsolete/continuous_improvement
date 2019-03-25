/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Commands.Validation;
using FluentValidation;

namespace Domain.Improvements
{
    /// <summary>
    /// Validates that we can make the improvement before attempting to do so
    /// </summary>
    public class InitiateImprovementBusinessValidator : CommandBusinessValidatorFor<InitiateImprovement>
    {
        private readonly ImprovementExists _improvementExists;
        private readonly ImprovableExists _improvableExists;

        /// <summary>
        /// Instantiates an <see cref="InitiateImprovementBusinessValidator" />
        /// </summary>
        /// <param name="improvementExists">delegate that checks if an Improvement Exists</param>
        /// <param name="improvableExists">delegate that checks if an Improvable Exists</param>
        public InitiateImprovementBusinessValidator(ImprovementExists improvementExists, ImprovableExists improvableExists)
        {
            _improvableExists = improvableExists;
            _improvementExists = improvementExists;

            //TODO: should be validating that the BoundedContext exists

            RuleFor(_ => _.Improvement)
                .Must(NotBeAnExistingImprovement).WithMessage(_ => $"Improvement '{_.Improvement}' already exists");
            RuleFor(_ => _.ForImprovable)
                .Must(BeAnExistingImprovable).WithMessage(_ => $"Improvable '{_.ForImprovable}' does not exist");
        }

        private bool BeAnExistingImprovable(ImprovableId id)
        {
            return _improvableExists(id);
        }

        private bool NotBeAnExistingImprovement(ImprovementId id)
        {
            return !_improvementExists(id);
        }
    }
}