using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Commands;
using Domain.SourceControl.GitHub;
using Infrastructure.Services.Github.Webhooks.EventPayloads;
using Infrastructure.Services.Github.Webhooks.Handling;
using Machine.Specifications;

namespace Core.Specs.SourceControl.GitHub.for_InstallationsWebhookHandler.when_handling_installation_repositories_event
{
    [Subject(typeof(ICanHandleGitHubWebhooks), "On(InstallationEventPayload)")]
    public class and_there_are_repository_additions_and_removals 
        : given.an_installations_webhook_handler_for<and_there_are_repository_additions_and_removals>
    {
        static InstallationRepositoriesEventPayload payload_event;
        static int installation_id;

        static IEnumerable<string> to_add;
        static IEnumerable<string> to_remove;
        static UpdateInstallationRepositories update_command;

        Establish context = () => 
        {
            installation_id = 100;
            to_add = new List<string>(new[]{ "first", "second"});
            to_remove = new List<string>(new[]{ "third", "fourth"});
            payload_event = new given.test_installation_repositories_event(installation_id,to_add,to_remove);
            //capture the update command so we can assert against the repositories
            command_coordinator.Setup(_ => _.Handle(Moq.It.Is<UpdateInstallationRepositories>(c => c.Id == installation_id)))
                //we need to use ICommand here, Moq doesn't understand UpdateInstallationRepositories
                .Callback<ICommand>(_ => update_command = (UpdateInstallationRepositories)_);
        };

        Because of = () => handler.On(payload_event);

        It should_update_the_installation_repositories = () => command_coordinator.Verify(_ => _.Handle(
            Moq.It.Is<UpdateInstallationRepositories>(c => c.Id == installation_id)),Moq.Times.Once()
        );
        
        It should_include_the_repositories_to_add = () => update_command.RepositoriesAdded.ShouldContainOnly(to_add.Select(s => (RepositoryFullName)s));
        It should_include_the_repositories_to_remove = () => update_command.RepositoriesRemoved.ShouldContainOnly(to_remove.Select(s => (RepositoryFullName)s));
    }
}