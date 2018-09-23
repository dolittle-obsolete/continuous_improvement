using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf.given
{
    public class an_empty_score
    {
        protected static ScoreOf<score_context> score;

        Establish context = () => score = new ScoreOf<score_context>(new score_context());       
    }
}