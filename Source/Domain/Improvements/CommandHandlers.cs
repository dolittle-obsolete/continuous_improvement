/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Handling;
using Dolittle.Domain;
namespace Domain.Improvements
{
    /// <inheritdoc />
    public class CommandHandlers : ICanHandleCommands
    {
        readonly IAggregateRootRepositoryFor<Improvement>  _aggregateRootRepoForImprovement;

        /// <summary>
        /// Instantiates an instance of <see cref="CommandHandlers" />
        /// </summary>
        /// <param name="aggregateRootRepoForImprovement">Aggregate Root Repository for <see cref="Improvement" /></param>
        public CommandHandlers(
            IAggregateRootRepositoryFor<Improvement>  aggregateRootRepoForImprovement            
        )
        {
             _aggregateRootRepoForImprovement =  aggregateRootRepoForImprovement;
        }

        /// <summary>
        /// Handles the <see cref="InitiateImprovement" /> command
        /// </summary>
        /// <param name="cmd">an <see cref="InitiateImprovement" /> command</param>
        public void Handle(InitiateImprovement cmd)
        {
            var improvement = _aggregateRootRepoForImprovement.Get(cmd.Improvement);
            improvement.Initiate(cmd.ForImprovable,cmd.Version,cmd.IsFromPullRequest);
        }
    }
}
