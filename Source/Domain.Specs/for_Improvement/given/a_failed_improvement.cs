/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Machine.Specifications;

namespace Domain.Specs.for_Improvement.given
{

    public class a_failed_improvement : an_initiated_improvement
    {
        Establish context = () => 
        {
            improvement.Fail();
            improvement.Commit();
        };
    }
}