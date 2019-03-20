using System.Linq;
using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;
using Policies.Specs.for_Improvements.for_Tracking;
using Policies.Specs.for_Improvements;

namespace Policies.Specs.for_Improvements.for_Tracking.when_tracking_a_step_status
{

    [Subject(typeof(IBuildStepsStatusTracker),"Track")]
    public class and_it_is_the_first_status_added_for_a_new_step : given.an_empty_tracker
    {
        static StepNumber existing_step;
        static StepNumber step;
        static StepStatus status; 

        Establish context = () => 
        {
            existing_step = 1;
            step = 2;
            status = StepStatus.NotStarted;
           
            tracker.Track(a.container_status_with(existing_step,status));
        };

        Because of = () => tracker.Track(a.container_status_with(step,status));

        It should_add_a_new_step_with_the_status = () => 
        {
            var tracked = tracker.Last();
            tracked.ShouldNotBeNull();
            tracked.Step.ShouldEqual(step);
            tracked.Statuses.First().ShouldEqual(status);
        };
        It should_have_two_steps = () => tracker.Count().ShouldEqual(2);
    }
}