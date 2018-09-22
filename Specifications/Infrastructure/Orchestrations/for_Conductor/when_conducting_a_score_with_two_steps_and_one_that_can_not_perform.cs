using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_Conductor
{
    public class when_conducting_a_score_with_two_steps_and_one_that_can_not_perform : given.a_conductor
    {
        class first_performer : performer {}
        class second_performer : performer {}

        static ScoreOf<score_context> score;

        static first_performer first_performer_instance;
        static second_performer second_performer_instance;

        
        Establish context = () => 
        {
            first_performer_instance = new first_performer();
            first_performer_instance.can_perform_result = false;
            second_performer_instance = new second_performer();
            
            container.Setup(_ => _.Get(typeof(first_performer))).Returns(first_performer_instance);
            container.Setup(_ => _.Get(typeof(second_performer))).Returns(second_performer_instance);
            score = new ScoreOf<score_context>(new score_context());
            score.AddStep<first_performer>();
            score.AddStep<second_performer>();
        };

        Because of = () => conductor.Conduct(score);

        It should_not_call_perform_on_first_performer = () => first_performer_instance.perform_called.ShouldBeFalse();       
        It should_call_perform_on_second_performer = () => second_performer_instance.perform_called.ShouldBeTrue();
    }
}