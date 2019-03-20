using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;

namespace Policies.Specs.for_Improvements.for_TrackedStepStatus.when_checking_has_been_handled
{
    [Subject(typeof(TrackedStepStatuses),"HasBeenHandled")]
    public class and_mark_as_handled_has_been_called
    {
        static TrackedStepStatuses tracked_statuses;

        Establish context = () => tracked_statuses = new TrackedStepStatuses(1,new[]{ StepStatus.Succeeded});

        Because of = () => tracked_statuses.MarkAsHandled();

        It should_be_handled = () => tracked_statuses.HasBeenHandled.ShouldBeTrue();
    }
}