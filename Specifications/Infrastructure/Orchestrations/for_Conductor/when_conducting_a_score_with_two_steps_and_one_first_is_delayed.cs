using System.Threading.Tasks;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_Conductor
{
    public class when_conducting_a_score_with_two_steps_and_one_first_is_delayed : given.a_conductor
    {
        static string last_performer = string.Empty;

        class first_performer : performer 
        {
            public override Task Perform(score_context score)
            {
                return Task.Run(() => 
                {
                    Task.Delay(500);
                    last_performer = "first";
                });
            }

        }
        class second_performer : performer 
        {
            public override Task Perform(score_context score)
            {
                last_performer = "second";
                return Task.CompletedTask;
            }

        }

        static ScoreOf<score_context> score;

        static first_performer first_performer_instance;
        static second_performer second_performer_instance;

        
        Establish context = () => 
        {
            first_performer_instance = new first_performer();
            second_performer_instance = new second_performer();
            
            container.Setup(_ => _.Get(typeof(first_performer))).Returns(first_performer_instance);
            container.Setup(_ => _.Get(typeof(second_performer))).Returns(second_performer_instance);
            score = new ScoreOf<score_context>(new score_context());
            score.AddStep<first_performer>();
            score.AddStep<second_performer>();
        };

        Because of = () => conductor.Conduct(score);

        It should_wait_for_first = () => last_performer.ShouldEqual("second");
    }
}