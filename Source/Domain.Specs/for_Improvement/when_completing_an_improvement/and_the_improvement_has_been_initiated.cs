
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Machine.Specifications.Events;
using Events.Improvements;
using Machine.Specifications;
using Domain.Improvements;

namespace Domain.Specs.for_Improvement.when_completing_an_improvement
{
    public class and_the_improvement_has_been_initiated : given.an_initiated_improvement
    {
        Because of = () => improvement.Complete();

        It should_fail_the_improvement = () => improvement.ShouldHaveEvent<ImprovementCompleted>()
                                                        .AtBeginning()
                                                        .Where(
                                                            e => e.ForImprovable.ShouldEqual(for_improvable.Value)
                                                        );
    }
}