
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class when_adding_a_step_that_does_not_need_configuration_and_gets_passed_configuration : given.an_empty_score
    {
        static configuration configuration_instance;
        static Exception result;

        Establish context = () => configuration_instance = new configuration();

        Because of = () => result = Catch.Exception(() => score.AddStep<performer,configuration>(configuration_instance));

        It should_hold_throw_performer_does_not_need_configuration = () => result.ShouldBeOfExactType<PerformerDoesNotNeedConfiguration>();
    }
}