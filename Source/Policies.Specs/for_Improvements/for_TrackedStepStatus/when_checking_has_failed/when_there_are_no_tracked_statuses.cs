using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_TrackedStepStatus.when_checking_has_failed
{
    [Subject(typeof(TrackedStepStatuses),"HasFailed")]
    public class when_there_are_no_tracked_statuses
    {
        static TrackedStepStatuses tracked_status;

        Establish context = () => tracked_status = new TrackedStepStatuses(1,new StepStatus[0]); 

        It should_not_have_sfailed = () => tracked_status.HasFailed.ShouldBeFalse();
    }
}