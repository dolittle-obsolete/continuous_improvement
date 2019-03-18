/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Core.SourceControl.GitHub;
using Dolittle.Commands;
using Domain.SourceControl.GitHub;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;

namespace Core.Specs.SourceControl.GitHub.for_InstallationsWebhookHandler.when_handling_installation_event
{
    [Subject(typeof(ICanHandleGitHubWebhooks), "On(InstallationEventPayload)")]
    public class and_it_is_not_a_delete_event : given.an_installations_webhook_handler_for<and_it_is_a_delete_event>
    {
        static InstallationEventPayload payload_event;
        static int installation_id;

        Establish context = () => 
        {
            installation_id = 100;
            payload_event = new given.test_installation_event(installation_id, "some-other-event");
        };

        Because of = () => handler.On(payload_event);

        It should_do_nothing = () => command_coordinator.Verify(_ => _.Handle(Moq.It.IsAny<ICommand>()),Moq.Times.Never());
        
    }
}