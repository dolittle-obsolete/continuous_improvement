using System.Threading.Tasks;

namespace Infrastructure.Orchestrations.for_Conductor
{
    public class performer : IPerformer<score_context>
    {            
        public bool can_perform_result = true;
        public bool can_perform_called = false;
        public score_context can_perform_score;
        public bool perform_called = false;
        public score_context perform_score;

        public bool CanPerform(score_context score)
        {
            can_perform_called = true;
            can_perform_score = score;
            return can_perform_result;
        }

        public virtual Task Perform(IPerformerLog log, score_context score)
        {
            perform_called = true;
            perform_score = score;
            return Task.CompletedTask;
        }
    }
}
        
 