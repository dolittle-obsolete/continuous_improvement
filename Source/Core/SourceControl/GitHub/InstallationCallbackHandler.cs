using System;
using Dolittle.Commands.Coordination;
using Domain.SourceControl.GitHub;
using Infrastructure.Services.Github.Installation;
using Microsoft.AspNetCore.Http;

namespace Core.SourceControl.GitHub
{
    public class InstallationCallbackHandler : ICanHandleInstallationCallbacks
    {
        readonly ICommandCoordinator _commandCoordinator;

        public InstallationCallbackHandler(
            ICommandCoordinator commandCoordinator
        )
        {
            _commandCoordinator = commandCoordinator;
        }

        public void Install(long installationId, HttpResponse response)
        {
            try {
                var commandResult = _commandCoordinator.Handle(new RegisterInstallation{
                    Id = installationId,
                });
                if (commandResult.Success)
                {
                    RedirectToInstallationFrontendPage(response, "InstallationSuccess/"+installationId);
                }
                else
                {
                    RedirectToInstallationFrontendPage(response, "InstallationError");
                }

            } catch (Exception ex) {
                // TODO: LOG
                RedirectToInstallationFrontendPage(response, "InstallationError");
            }
        }

        public void Update(long installationId, HttpResponse response)
        {
            // We get the same events through the webhook, but we could show something here?
            RedirectToInstallationFrontendPage(response, "InstallationUpdated/"+installationId);
        }

        static void RedirectToInstallationFrontendPage(HttpResponse response, string extra = "")
        {
            response.StatusCode = StatusCodes.Status303SeeOther;
            response.Headers["Location"] = "http://localhost:8080/GitHub/Repositories/" + extra;
        }
    }
}