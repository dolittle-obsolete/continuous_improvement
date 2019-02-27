using Machine.Specifications;
using Domain;
using Domain.Improvements;
using System;
using Concepts;
using Concepts.Improvements;
using Concepts.Improvables;
using Dolittle.Machine.Specifications.Events;

namespace Domain.Specs.for_Improvement.when_initiating_an_improvement
{
    [Subject(typeof(Domain.Improvements.Improvement), "Initiate")]
    public class and_the_improvement_has_not_been_initiated
    {
        static Domain.Improvements.Improvement improvement;
        static ImprovableId for_improvable;
        static Concepts.Version version;

        Establish context = () => 
        {
            version = "1.1.0";
            for_improvable = Guid.NewGuid();
            improvement = new Domain.Improvements.Improvement(Guid.NewGuid());
        };

        Because of = () => improvement.Initiate(for_improvable, version, isFromPullRequest:false);

        It should_have_initialized_the_improvement = () => improvement.ShouldHaveEvent<Events.Improvements.ImprovementInitiated>().AtBeginning();
    }
}