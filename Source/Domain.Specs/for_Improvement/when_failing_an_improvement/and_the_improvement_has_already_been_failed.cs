
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Machine.Specifications.Events;
using Events.Improvements;
using Machine.Specifications;
using Domain.Improvements;

namespace Domain.Specs.for_Improvement.when_failing_an_improvement
{
    public class and_the_improvement_has_already_been_failed : given.a_failed_improvement
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => improvement.Fail());

        It should_not_fail_the_improvement = () => improvement.ShouldHaveAnEmptyStream();
        It should_indicate_that_the_improvement_has_already_failed = () => exception.ShouldBeOfExactType<ImprovementAlreadyFailed>();
    }
}