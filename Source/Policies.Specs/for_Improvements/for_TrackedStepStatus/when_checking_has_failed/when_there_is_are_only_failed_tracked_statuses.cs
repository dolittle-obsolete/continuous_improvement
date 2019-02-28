using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_TrackedStepStatus.when_checking_has_failed
{
    [Subject(typeof(TrackedStepStatuses),"HasFailed")]
    public class when_there_is_are_only_failed_tracked_statuses
    {
        static TrackedStepStatuses tracked_status;

        Establish context = () => tracked_status = new TrackedStepStatuses(1,new []{ StepStatus.Failed, StepStatus.Failed, StepStatus.Failed, StepStatus.Failed}); 

        It should_be_failed = () => tracked_status.HasFailed.ShouldBeTrue();
    }
}