using Concepts.Improvements;
using Machine.Specifications;
using Moq;
using Policies.Improvements;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_Tracking.given
{
    public class an_empty_tracker
    {
        protected static IBuildStepsStatusTracker tracker;
        Establish context = () => tracker = new BuildStepsStatusTracker();

        protected static IContainerStatus BuildStatusFor(int step, StepStatus status)
        {
            var mock = new Mock<IContainerStatus>();
            mock.SetupGet(_ => _.IsBuildContainer).Returns(true);
            mock.SetupGet(_ => _.Step).Returns(new StepId(step,0,"test"));
            mock.SetupGet(_ => _.Status).Returns(status);
            return mock.Object;
        }  
    }
}