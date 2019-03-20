using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Concepts;
using Dolittle.Tenancy;
using FluentValidation;

namespace Domain.Improvements.Metadata
{
    public class ImprovementMetadata : Value<ImprovementMetadata>
    {
        public ImprovementMetadata(TenantId tenant, RecipeType recipe, ImprovementId improvement, ImprovableId improvementFor, Version version)
        {
            Tenant = tenant;
            Recipe = recipe;
            Improvement = improvement;
            ImprovementFor = improvementFor;
            Version = version;

        }
        public TenantId Tenant { get; }
        public RecipeType Recipe { get; }
        public ImprovementId Improvement { get; }

        public ImprovableId ImprovementFor { get; }
        public Version Version { get; }
    }
}