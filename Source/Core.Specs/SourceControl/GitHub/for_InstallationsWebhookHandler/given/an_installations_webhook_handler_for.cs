/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;
using Moq;
using Core.SourceControl.GitHub;
using Dolittle.Commands.Coordination;

namespace Core.Specs.SourceControl.GitHub.for_InstallationsWebhookHandler.given
{
    public class an_installations_webhook_handler_for<T>
    {
        protected static InstallationsWebhookHandler handler;
        protected static Mock<ICommandCoordinator> command_coordinator;

        Establish context = () =>
        {
            command_coordinator = new Mock<ICommandCoordinator>();
            handler = new InstallationsWebhookHandler(command_coordinator.Object);
        };
    }
}