using Machine.Specifications;
using Domain;
using Domain.Improvements;
using System;
using Concepts;
using Concepts.Improvements;
using Concepts.Improvables;
using Dolittle.Machine.Specifications.Events;
using Events.Improvements;

namespace Domain.Specs.for_Improvement.when_initiating_an_improvement
{
    [Subject(typeof(Domain.Improvements.Improvement), "Initiate")]
    public class and_the_improvement_has_already_been_initiated
    {
        static Domain.Improvements.Improvement improvement;
        static ImprovableId for_improvable;
        static Concepts.Version version;
        static Exception exception;

        Establish context = () => 
        {
            version = "1.1.0";
            for_improvable = Guid.NewGuid();
            improvement = new Domain.Improvements.Improvement(Guid.NewGuid());
            improvement.Initiate(for_improvable, version, isFromPullRequest:false);
            improvement.Commit();
        };

        Because of = () => exception = Catch.Exception(() => improvement.Initiate(for_improvable, version, isFromPullRequest:false));

        It should_not_have_initialized_the_improvement = () => improvement.ShouldNotHaveEvent<ImprovementInitiated>();
        It should_have_failed_and_indicated_that_the_improvement_has_already_been_initiated = () => exception.ShouldBeOfExactType<ImprovementAlreadyInitiated>();

    }
}