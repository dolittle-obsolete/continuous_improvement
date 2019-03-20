using System;
using System.Collections.Generic;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Tenancy;
using Domain.Improvements.Metadata;
using Machine.Specifications;
using Domain.Specs.for_Improvement.for_metadata;

namespace Domain.Specs.for_Improvement.for_metadata.when_building
{

    [Subject(typeof(IImprovementMetadataFactory), "Build")]
    public class with_valid_metadata : given.a_factory
    {
        static ImprovementMetadata result;
        static IDictionary<string,string> metadata_values;

        Establish context = () => metadata_values = metadata.get_valid();

        Because of = () => result = factory.BuildFrom(metadata_values, "a test");

        It should_build_the_metadata = () => result.ShouldNotBeNull();
        It should_have_the_correct_tenant = () => result.Tenant.ShouldEqual((TenantId)Guid.Parse(metadata.valid_tenant_id));
        It should_have_the_correct_improvable = () => result.ImprovementFor.ShouldEqual((ImprovableId)Guid.Parse(metadata.valid_improvement_for));
        It should_have_the_correct_improvement = () => result.Improvement.ShouldEqual((ImprovementId)Guid.Parse(metadata.valid_improvement_id));
        It should_have_the_correct_recipe_type = () => result.Recipe.ShouldEqual((RecipeType)metadata.valid_recipe_type);
        It should_have_the_correct_version = () => result.Version.ShouldEqual((Concepts.Version)metadata.valid_version);
    }
}
