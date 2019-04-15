/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Domain.Improvables;
using Machine.Specifications;
using Moq;

namespace Domain.Specs.for_Improvable.when_registering.when_handling_the_register_command.given
{
    public class a_command_handler_for<T>
    {
        protected static EventSourceId id;
        protected static Mock<IAggregateRootRepositoryFor<Improvable>> repo; 
        protected static CommandHandler command_handler;
        protected static Improvable improvable;

        Establish context = () => 
        {
            id = Guid.NewGuid();
            improvable = new Improvable(id);
            repo = new Mock<IAggregateRootRepositoryFor<Improvable>>();
            repo.Setup(_ => _.Get(Moq.It.Is<EventSourceId>(es => es == id))).Returns(improvable);
            command_handler = new CommandHandler(repo.Object);
        };
    }
}