/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Booting;
using Dolittle.Logging;
using Dolittle.Types;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Machine.Specifications;
using Moq;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_Bootstrapping.given
{
    public class a_bootstrapper
    {
        protected static Mock<ILogger> logger;
        protected static Mock<IImplementationsOf<ICanHandleGitHubWebhooks>> handlers_implementations;
        protected static Mock<IWebhookCoordinator> webhook_coordinator;
        protected static ICanPerformBootProcedure bootstrapper;
        protected static Mock<first_handler> first_handler;
        protected static Mock<second_handler> second_handler;
        protected static Mock<handler_with_no_implementations> handler_with_no_implementations;
        protected static List<ICanHandleGitHubWebhooks> handlers;

        Establish context = () => 
        {
            first_handler = new Mock<first_handler>();
            second_handler = new Mock<second_handler>();
            handler_with_no_implementations = new Mock<handler_with_no_implementations>();
            handlers = new List<ICanHandleGitHubWebhooks>();
            handlers.Add(first_handler.Object);
            handlers.Add(second_handler.Object);
            handlers.Add(handler_with_no_implementations.Object);
            handlers_implementations = new Mock<IImplementationsOf<ICanHandleGitHubWebhooks>>();
            handlers_implementations.Setup(_ => _.GetEnumerator()).Returns(handlers.Select(h => h.GetType()).GetEnumerator());

            webhook_coordinator = new Mock<IWebhookCoordinator>();
            logger = new Mock<ILogger>();

            bootstrapper = new Bootstrapping(logger.Object,handlers_implementations.Object,webhook_coordinator.Object);
        };

        protected static Type IsType<T>()
        {
            return Moq.It.Is<Type>(t => typeof(T).IsAssignableFrom(t));
        }
    }

    public interface first_handler : ICanHandleGitHubWebhooks
    {
        void On(CreateEventPayload payload);
        void On(DeleteEventPayload payload);
    }

    public interface second_handler : ICanHandleGitHubWebhooks
    {
        void On(InstallationEventPayload payload);
        void On(InstallationRepositoriesEventPayload payload);
        void On(DeleteEventPayload payload);
    }

    public interface handler_with_no_implementations : ICanHandleGitHubWebhooks
    {
    }
}