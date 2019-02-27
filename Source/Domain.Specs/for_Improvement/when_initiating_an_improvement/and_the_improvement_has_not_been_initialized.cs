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
    public class and_the_improvement_has_not_been_initiated : given.an_uninitiated_improvement
    {
        Because of = () => improvement.Initiate(for_improvable, version, isFromPullRequest:false);

        It should_have_initialized_the_improvement = () => improvement.ShouldHaveEvent<Events.Improvements.ImprovementInitiated>()
                                                            .AtBeginning()
                                                            .Where(
                                                                e => e.Version.ShouldEqual(version.Value),
                                                                e => e.ForImprovable.ShouldEqual(for_improvable.Value),
                                                                e => e.PullRequest.ShouldBeFalse()
                                                            );
    }
}