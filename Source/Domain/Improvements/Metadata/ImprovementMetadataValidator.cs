/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Concepts;
using Dolittle.Tenancy;
using FluentValidation;

namespace Domain.Improvements.Metadata
{
    /// <summary>
    /// Validates the metadata describing the improvement
    /// </summary>
    /// <typeparam name="ImprovementMetadata"></typeparam>
    public class ImprovementMetadataValidator : AbstractValidator<ImprovementMetadata>
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="ImprovementMetadataValidator" />
        /// </summary>
        public ImprovementMetadataValidator()
        {
            RuleFor(_ => _.Tenant)
                .NotNull().WithMessage("Tenant is required");
            RuleFor(_ => _.Tenant.Value).NotEmpty().WithMessage("Tenant is required").When(_ => _ != null);
            RuleFor(_ => _.Recipe).MustBeAValidRecipeType();
            RuleFor(_ => _.Improvement).MustBeAValidImprovementId();
            RuleFor(_ => _.ImprovementFor).MustBeAValidImprovableId();
            RuleFor(_ => _.Version).MustBeAValidVersion();
        }
    }
}
