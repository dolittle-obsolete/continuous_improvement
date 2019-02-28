/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Moq;
using Policies.Improvements;
using Policies.Improvements.StepHandling;
using Domain.Improvements.Metadata;
using System;

namespace Policies.Specs.for_Improvements.for_handling_build_steps.given
{
    public class a_handle_build_steps_instance
    {
        protected static IHandleBuildSteps handle_build_steps;
        protected static Mock<IImprovementStepResultHandler> step_result_handler;
        protected static ImprovementMetadata metadata;

        Establish context = () =>
        { 
            metadata = new ImprovementMetadata(Guid.NewGuid(),"an african swallow", Guid.NewGuid(),Guid.NewGuid(),"1.1.1");
            step_result_handler = new Mock<IImprovementStepResultHandler>();
            handle_build_steps = new HandleBuildSteps(() => step_result_handler.Object);
        };
    }
}