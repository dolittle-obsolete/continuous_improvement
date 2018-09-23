
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class when_adding_a_step_that_needs_configuration_without_passing_it_configuration : given.an_empty_score
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => score.AddStep<performer_with_configuration>());

        It should_throw_performer_needs_configuration = () => result.ShouldBeOfExactType<PerformerNeedsConfiguration>();
    }
}