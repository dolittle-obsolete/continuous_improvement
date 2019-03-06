/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using System.Collections.Generic;
using Concepts.Improvables;
using Concepts.Improvements;
using Machine.Specifications;
using Moq;
using Policies.Improvements;
using Policies.Improvements.StepHandling;
using Policies.Improvements.Tracking;
using It = Machine.Specifications.It;
using Policies.Specs.for_Improvements;
using given = Policies.Specs.for_Improvements;

namespace Policies.Specs.for_Improvements.for_handling_build_steps
{
    [Subject(typeof(IHandleBuildSteps), "Handle")]
    public class when_handling_tracked_build_steps : given.a_handle_build_steps_instance
    {
        static BuildStepsStatusTracker tracked_statuses;
        static int first_succeeded = 1;
        static int second_succeeded = 2;
        static int first_failed = 3;
        static int second_failed = 4;
        static int first_in_progress = 5;
        static int second_in_progress = 6;

        Establish context = () => 
        {
            tracked_statuses = new BuildStepsStatusTracker();
            tracked_statuses.Track(a.container_status_with(first_succeeded,StepStatus.Succeeded));
            tracked_statuses.Track(a.container_status_with(second_succeeded,StepStatus.Succeeded));
            tracked_statuses.Track(a.container_status_with(first_failed,StepStatus.Failed));
            tracked_statuses.Track(a.container_status_with(second_failed,StepStatus.Failed));
            tracked_statuses.Track(a.container_status_with(first_in_progress,StepStatus.InProgress));
            tracked_statuses.Track(a.container_status_with(second_in_progress,StepStatus.InProgress));
        };

        Because of = () => handle_build_steps.Handle(metadata,tracked_statuses);

        It should_handle_the_failed_steps = () => {
                step_result_handler.Verify(_ 
                    => _.HandleFailedStep(metadata.Recipe,first_failed,metadata.Improvement,metadata.ImprovementFor,metadata.Version),
                    times:Moq.Times.Once);
                
                step_result_handler.Verify(_ 
                    => _.HandleFailedStep(metadata.Recipe,second_failed,metadata.Improvement,metadata.ImprovementFor,metadata.Version),
                    times:Moq.Times.Once); 
        };
        It should_handle_the_succeeded_steps = () => {
                step_result_handler.Verify(_ 
                    => _.HandleSuccessfulStep(metadata.Recipe,first_succeeded,metadata.Improvement,metadata.ImprovementFor,metadata.Version),
                    times:Moq.Times.Once);
                
                step_result_handler.Verify(_ 
                    => _.HandleSuccessfulStep(metadata.Recipe,second_succeeded,metadata.Improvement,metadata.ImprovementFor,metadata.Version),
                    times:Moq.Times.Once); 
        };    

        It should_not_handle_the_inconclusive_steps = () => {
                step_result_handler.Verify(_ 
                    => _.HandleSuccessfulStep(
                        Moq.It.IsAny<RecipeType>(),
                        first_in_progress,
                        Moq.It.IsAny<ImprovementId>(),
                        Moq.It.IsAny<ImprovableId>(),
                        Moq.It.IsAny<Concepts.Version>()),
                    times:Moq.Times.Never);
                
                step_result_handler.Verify(_ 
                    => _.HandleSuccessfulStep(
                        Moq.It.IsAny<RecipeType>(),
                        second_in_progress,
                        Moq.It.IsAny<ImprovementId>(),
                        Moq.It.IsAny<ImprovableId>(),
                        Moq.It.IsAny<Concepts.Version>()),
                    times:Moq.Times.Never);

                step_result_handler.Verify(_ 
                    => _.HandleFailedStep(
                        Moq.It.IsAny<RecipeType>(),
                        first_in_progress,
                        Moq.It.IsAny<ImprovementId>(),
                        Moq.It.IsAny<ImprovableId>(),
                        Moq.It.IsAny<Concepts.Version>()),
                    times:Moq.Times.Never);
                
                step_result_handler.Verify(_ 
                    => _.HandleFailedStep(
                        Moq.It.IsAny<RecipeType>(),
                        second_in_progress,
                        Moq.It.IsAny<ImprovementId>(),
                        Moq.It.IsAny<ImprovableId>(),
                        Moq.It.IsAny<Concepts.Version>()),
                    times:Moq.Times.Never);
        };                             
    }
}