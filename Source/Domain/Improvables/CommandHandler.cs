/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Handling;
using Dolittle.Domain;

namespace Domain.Improvables
{
    /// <summary>
    /// Command Handler for handling commands targetting the <see cref="Improvable" />
    /// </summary>
    public class CommandHandler : ICanHandleCommands
    {
        readonly IAggregateRootRepositoryFor<Improvable>  _aggregateRootRepoForImprovable;

        /// <summary>
        /// Instantiates a new instance of a <see cref="CommandHandler" />
        /// </summary>
        /// <param name="aggregateRootRepoForImprovable"></param>
        public CommandHandler(
            IAggregateRootRepositoryFor<Improvable>  aggregateRootRepoForImprovable            
        )
        {
             _aggregateRootRepoForImprovable =  aggregateRootRepoForImprovable;
        }

        /// <summary>
        /// Handles the registration of a new improvable
        /// </summary>
        /// <param name="cmd">Details of the new improvable to be registered</param>
        public void Handle(RegisterImprovable cmd)
        {
            var improvable = _aggregateRootRepoForImprovable.Get(cmd.Improvable.Value);
            improvable.Register(cmd.Name,cmd.Recipe,cmd.Repository,cmd.Path);
        }
    }
}
