/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.Improvements;
using Machine.Specifications;
using Moq;
using Policies.Improvements;
using Policies.Improvements.Tracking;
using It = Machine.Specifications.It;

namespace Policies.Specs.for_Improvements.for_BuildPodProcessor.when_processing
{
    [Subject(typeof(IBuildPodProcessor),"Process")]
    public class a_pod_which_has_succeeded : given.a_build_pod_processor
    {
        static Mock<IPod> pod;
        static List<IContainerStatus> container_statuses;

        Establish context = () => 
        {
            pod = new Mock<IPod>();
            pod.SetupGet(_ => _.Metadata).Returns(metadata);
            pod.SetupGet(_ => _.HasStatuses).Returns(true);
            pod.SetupGet(_ => _.HasSucceeded).Returns(true);
            pod.SetupGet(_ => _.HasFailed).Returns(false);

            container_statuses = new List<IContainerStatus>();
            var first = new Mock<IContainerStatus>();
            first.SetupGet(_ => _.Step).Returns(new Concepts.Improvements.StepId(1,1,"first"));
            first.Setup(_ => _.GetStatus()).Returns(StepStatus.InProgress);
            var second = new Mock<IContainerStatus>();
            second.SetupGet(_ => _.Step).Returns(new Concepts.Improvements.StepId(2,1,"second"));
            second.Setup(_ => _.GetStatus()).Returns(StepStatus.NotStarted);
            container_statuses.Add(first.Object);
            container_statuses.Add(second.Object);
            pod.SetupGet(_ => _.Statuses).Returns(container_statuses);
        };

        Because of = () => processor.Process(pod.Object);

        It should_log_that_the_pod_has_succeeded = () => VerifyLoggedInformationMessageContains("succeeded");
        It should_track_the_statuses = () => {
            status_tracker.Verify(_ => _.Track(1,StepStatus.InProgress), Times.Once());
            status_tracker.Verify(_ => _.Track(2,StepStatus.NotStarted), Times.Once());
        };
        It should_handle_the_steps = () => handle_build_steps.Verify(_ => _.Handle(metadata,status_tracker.Object), Times.Once());
        It should_delete_the_pod = () => pod.Verify(_ => _.Delete(),Times.Once());
    }
}