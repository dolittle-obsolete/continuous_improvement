/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Execution;
using Dolittle.Logging;
using Domain.Improvements.Metadata;
using Machine.Specifications;
using Moq;
using Policies.Improvements;
using Policies.Improvements.StepHandling;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_BuildPodProcessor.given
{
    public class a_build_pod_processor
    {
        protected static IBuildPodProcessor processor;
        protected static Mock<ILogger> logger;
        protected static Mock<IHandleBuildSteps> handle_build_steps;
        protected static Mock<IBuildStepsStatusTracker> status_tracker;
        protected static Mock<IExecutionContextManager> execution_context_manager;
        protected static ImprovementMetadata metadata;

        Establish context = () => 
        {
            metadata = new ImprovementMetadata(Guid.NewGuid(),"a test", Guid.NewGuid(),Guid.NewGuid(),"1.1.0");
            execution_context_manager = new Mock<IExecutionContextManager>();
            logger = new Mock<ILogger>();
            handle_build_steps = new Mock<IHandleBuildSteps>();
            status_tracker = new Mock<IBuildStepsStatusTracker>();
            processor = new BuildPodProcessor(execution_context_manager.Object, handle_build_steps.Object,() => status_tracker.Object,logger.Object);
        };

        protected static void VerifyLoggedInformationMessageContains(string toVerify)
        {
            logger.Verify(_ => _.Information(Moq.It.Is<string>(s => s.Contains(toVerify)),Moq.It.IsAny<string>(),Moq.It.IsAny<int>(),Moq.It.IsAny<string>()));
        }

        protected static void VerifyLoggedWarningMessageContains(string toVerify)
        {
            logger.Verify(_ => _.Warning(Moq.It.Is<string>(s => s.Contains(toVerify)),Moq.It.IsAny<string>(),Moq.It.IsAny<int>(),Moq.It.IsAny<string>()));
        }
    }
}