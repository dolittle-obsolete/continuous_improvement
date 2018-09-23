
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class when_adding_a_step_with_configuration : given.an_empty_score
    {
        static configuration configuration_instance;

        Establish context = () => configuration_instance = new configuration();

        Because of = () => score.AddStep<performer_with_configuration,configuration>(configuration_instance);

        It should_hold_one_step = () => score.Steps.Count().ShouldEqual(1);
        It should_hold_the_type_in_step = () => score.Steps.First().Type.ShouldEqual(typeof(performer_with_configuration));
        It should_hold_the_configuration_in_step = () => score.Steps.First().Configuration.ShouldEqual(configuration_instance);
    }
}