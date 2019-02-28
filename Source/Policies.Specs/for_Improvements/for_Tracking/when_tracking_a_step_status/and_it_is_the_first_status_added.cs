using System.Linq;
using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;
using Policies.Specs.for_Improvements.for_Tracking;

namespace Policies.Specs.for_Improvements.for_Tracking.when_tracking_a_step_status
{

    [Subject(typeof(IBuildStepsStatusTracker),"Track")]
    public class and_it_is_the_first_status_added : given.an_empty_tracker
    {
        static StepNumber step;
        static StepStatus status; 

        Establish context = () => 
        {
            step = new StepNumber(1);
            status = StepStatus.NotStarted;
        };

        Because of = () => tracker.Track(step,status);

        It should_add_a_new_step_with_the_status = () => 
        {
            var tracked = tracker.First();
            tracked.ShouldNotBeNull();
            tracked.Step.ShouldEqual(step);
            tracked.Statuses.First().ShouldEqual(status);
        };
        It should_not_have_any_other_steps = () => tracker.Count().ShouldEqual(1);
    }
}