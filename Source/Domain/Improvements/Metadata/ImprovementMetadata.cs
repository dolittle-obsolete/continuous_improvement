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
    /// Encapsulates the metadata associated with an <see cref="Improvement" /> 
    /// </summary>
    public class ImprovementMetadata : Value<ImprovementMetadata>
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ImprovementMetadata" />
        /// </summary>
        /// <param name="tenant">The tenant</param>
        /// <param name="recipe">The type of the recipe that describes the build steps</param>
        /// <param name="improvement">The id of the improvment</param>
        /// <param name="improvementFor">The id of the improvable being improved</param>
        /// <param name="version">The version of the software that is associated with this improvement</param>
        public ImprovementMetadata(TenantId tenant, RecipeType recipe, ImprovementId improvement, ImprovableId improvementFor, Version version)
        {
            Tenant = tenant;
            Recipe = recipe;
            Improvement = improvement;
            ImprovementFor = improvementFor;
            Version = version;

        }
        /// <summary>
        /// The tenant
        /// </summary>
        public TenantId Tenant { get; }
        /// <summary>
        /// The type of the recipe that describes the build steps
        /// </summary>
        public RecipeType Recipe { get; }
        /// <summary>
        /// The id of the improvment
        /// </summary>
        public ImprovementId Improvement { get; }
        /// <summary>
        /// The id of the improvable being improved
        /// </summary>
        public ImprovableId ImprovementFor { get; }
        /// <summary>
        /// The version of the software that is associated with this improvement
        /// </summary>
        public Version Version { get; }
    }
}