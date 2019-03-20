/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Core.SourceControl.GitHub;
using Dolittle.Commands.Coordination;
using Infrastructure.Services.Github.Installation;
using Machine.Specifications;
using Moq;

namespace Core.Specs.SourceControl.GitHub.for_InstallationCallbackHandler.given
{
    public class an_installation_callback_handler_for<T>
    {
        protected static ICanHandleInstallationCallbacks handler;
        protected static Mock<ICommandCoordinator> command_coordinator;
        protected static string base_url;

        Establish context = () =>
        {
            base_url = "http://localhost:8080/GitHub/Repositories/";
            command_coordinator = new Mock<ICommandCoordinator>();
            handler = new InstallationCallbackHandler(command_coordinator.Object, base_url);
        };
    }
}