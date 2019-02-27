using System;
using System.Collections.Generic;
using Domain.Improvements.Metadata;
using Machine.Specifications;

namespace Domain.Specs.for_Improvement.for_metadata.when_building
{
    [Subject(typeof(IImprovementMetadataFactory), "Build")]
    public class with_missing_values : given.a_factory
    {
        static IDictionary<string,string> metadata_values;

        static Exception exception;

        Establish context = () => 
        {
            metadata_values = metadata.get_valid();
            metadata_values.remove(Constants.Tenant, Constants.Version);
        };

        Because of = () => exception = Catch.Exception(() => factory.BuildFrom(metadata_values, "a test"));

        It should_not_build_the_metadata = () => exception.ShouldNotBeNull();
        It should_throw_an_invalid_metadata_exception = () => exception.ShouldBeOfExactType<InvalidImprovementMetadata>();
        It should_indicate_which_metadata_elements_are_invalid = () => {
            Console.WriteLine(exception.Message);
            exception.Message.Contains(Constants.Tenant).ShouldBeTrue();
            exception.Message.Contains(Constants.Version).ShouldBeTrue();
        };
        It should_not_include_metadata_elements_that_are_valid = () => {
            exception.Message.Contains(Constants.Improvable).ShouldBeFalse();
            exception.Message.Contains(Constants.Improvement).ShouldBeFalse();
            exception.Message.Contains(Constants.RecipeType).ShouldBeFalse();
        };
    }
}
