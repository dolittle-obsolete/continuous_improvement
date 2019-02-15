using Dolittle.Commands.Handling;
using Dolittle.Domain;
namespace Domain.Improvements
{
    public class CommandHandlers : ICanHandleCommands
    {
        readonly IAggregateRootRepositoryFor<Improvement>  _aggregateRootRepoForImprovement;

        public CommandHandlers(
            IAggregateRootRepositoryFor<Improvement>  aggregateRootRepoForImprovement            
        )
        {
             _aggregateRootRepoForImprovement =  aggregateRootRepoForImprovement;
        }

        public void Handle(InitiateImprovement cmd)
        {
            var improvement = _aggregateRootRepoForImprovement.Get(cmd.Improvement);
            improvement.Initiate(cmd.ForImprovable,cmd.Version,cmd.IsFromPullRequest);
        }
    }
}
