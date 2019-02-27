
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Dolittle.Machine.Specifications.Events;
using Moq;
using Machine.Specifications;
using Dolittle.Domain;
using It = Machine.Specifications.It;
using Events.Improvements;

namespace Domain.Specs.for_Improvement.for_command_handler
{
    [Subject(typeof(Domain.Improvements.CommandHandlers), "handle initiate improvement")]
    public class when_initiating_an_improvement : given.an_uninitiated_improvement
    {
        static Mock<IAggregateRootRepositoryFor<Domain.Improvements.Improvement>> repository;
        static Domain.Improvements.CommandHandlers command_handlers;

        static Domain.Improvements.InitiateImprovement initiate_improvement;

        Establish context = () => 
        {
            initiate_improvement = new Improvements.InitiateImprovement
            {
                Improvement = improvement_id,
                ForImprovable = for_improvable,
                Version = version,
                IsFromPullRequest = true
            };

            repository = new Mock<IAggregateRootRepositoryFor<Improvements.Improvement>>();
            repository.Setup(_ => _.Get(improvement_id)).Returns(improvement);
            command_handlers = new Improvements.CommandHandlers(repository.Object);
        };

        Because of = () => command_handlers.Handle(initiate_improvement);

        It should_fetch_the_improvement_aggregate = () => repository.Verify(_ => _.Get(improvement_id),Times.Once);
        It should_initiate_the_correct_improvement = () => improvement.ShouldHaveEvent<ImprovementInitiated>()
                                                            .AtBeginning()
                                                            .WithValues(
                                                                _ => _.Version == version,
                                                                _ => _.ForImprovable == for_improvable
                                                            );
    }
}