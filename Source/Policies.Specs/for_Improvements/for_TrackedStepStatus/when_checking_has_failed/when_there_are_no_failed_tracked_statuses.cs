using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_TrackedStepStatus.when_checking_has_failed
{
    [Subject(typeof(TrackedStepStatuses),"HasFailed")]
    public class when_there_are_no_failed_tracked_statuses
    {
        static TrackedStepStatuses tracked_status;

        Establish context = () => tracked_status = new TrackedStepStatuses(1,new []{ StepStatus.InProgress, StepStatus.NotStarted, StepStatus.Succeeded}); 

        It should_not_be_failed = () => tracked_status.HasFailed.ShouldBeFalse();
    }
}