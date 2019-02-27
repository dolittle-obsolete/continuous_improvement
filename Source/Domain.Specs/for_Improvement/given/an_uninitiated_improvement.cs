/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Machine.Specifications;

namespace Domain.Specs.for_Improvement.given
{
    public class an_uninitiated_improvement
    {
        protected static Domain.Improvements.Improvement improvement;
        protected static Concepts.Version version;
        protected static Concepts.Improvables.ImprovableId for_improvable;

        protected static Concepts.Improvements.ImprovementId improvement_id;

        Establish context = () => 
        {
            version = "1.1.0";
            for_improvable = Guid.NewGuid(); 
            improvement_id = Guid.NewGuid();
            improvement = new Improvements.Improvement(improvement_id);
        };
    }
}