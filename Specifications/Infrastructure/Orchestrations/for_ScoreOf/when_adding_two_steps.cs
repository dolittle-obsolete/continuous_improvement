
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class when_adding_two_steps : given.an_empty_score
    {
        static configuration configuration_instance;

        Establish context = () => configuration_instance = new configuration();

        Because of = () => 
        {
            score.AddStep<performer_with_configuration,configuration>(configuration_instance);
            score.AddStep<performer_with_configuration,configuration>(configuration_instance);
        };

        It should_hold_two_steps = () => score.Steps.Count().ShouldEqual(2);
        It should_set_number_to_one_for_first_step = () => score.Steps.First().Number.Value.ShouldEqual(1);
        It should_set_number_to_two_for_second_step = () => score.Steps.ToArray()[1].Number.Value.ShouldEqual(2);
    }
}