using System.Linq;
using Concepts.Improvements;
using Machine.Specifications;
using Policies.Improvements.Tracking;
using Policies.Specs.for_Improvements.for_Tracking;

namespace Policies.Specs.for_Improvements.for_Tracking.when_tracking_a_step_status
{
    [Subject(typeof(IBuildStepsStatusTracker),"Track")]
    public class and_it_is_the_second_status_added_for_that_step : given.an_empty_tracker
    {
        static StepNumber step;
        static StepStatus first_status; 
        static StepStatus second_status;

        Establish context = () => 
        {
            step = 2;
            first_status = StepStatus.NotStarted;
            second_status = StepStatus.InProgress;
            tracker.Track(step,first_status);
        };

        Because of = () => tracker.Track(step,second_status);

        It should_not_add_a_new_step = () =>  tracker.Count().ShouldEqual(1);
        It should_add_the_status_to_the_existing_step = () => tracker.First().Statuses.ShouldContainOnly(new[]{ first_status, second_status});
    }
}