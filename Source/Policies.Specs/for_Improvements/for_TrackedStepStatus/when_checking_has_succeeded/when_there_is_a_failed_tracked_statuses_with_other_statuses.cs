using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_TrackedStepStatus.when_checking_has_succeeded
{
    [Subject(typeof(TrackedStepStatuses),"HasSucceeded")]
    public class when_there_is_a_succeeded_tracked_statuses_with_other_statuses
    {
        static TrackedStepStatuses tracked_status;

        Establish context = () => tracked_status = new TrackedStepStatuses(1,new []{ StepStatus.InProgress, StepStatus.NotStarted, StepStatus.Succeeded, StepStatus.Failed}); 

        It should_not_have_succeeded = () => tracked_status.HasSucceeded.ShouldBeFalse();
    }
}