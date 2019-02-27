/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Machine.Specifications;

namespace Domain.Specs.for_Improvement.given
{

    public class an_initiated_improvement : an_uninitiated_improvement
    {
        Establish context = () => 
        {
            improvement.Initiate(for_improvable,version,isFromPullRequest: false);
            improvement.Commit();
        };
    }
}