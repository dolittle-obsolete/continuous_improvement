using Concepts.Improvements;
using Moq;
using Policies.Improvements;

namespace Policies.Specs.for_Improvements
{
    public static class a
    {
        public static IContainerStatus container_status_with(int step, StepStatus status)
        {
            var mock = new Mock<IContainerStatus>();
            mock.SetupGet(_ => _.IsBuildContainer).Returns(true);
            mock.SetupGet(_ => _.Step).Returns(new StepId(step,0,"test"));
            mock.SetupGet(_ => _.Status).Returns(status);
            return mock.Object;
        } 
    }
}