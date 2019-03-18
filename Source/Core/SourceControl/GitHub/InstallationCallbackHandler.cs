/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Commands.Coordination;
using Domain.SourceControl.GitHub;
using Infrastructure.Services.Github.Installation;
using Microsoft.AspNetCore.Http;

namespace Core.SourceControl.GitHub
{
    /// <summary>
    /// An implemenation of <see cref="ICanHandleInstallationCallbacks" />
    /// </summary>
    public class InstallationCallbackHandler : ICanHandleInstallationCallbacks
    {
        public const string SUCCESS = "InstallationSuccess/";
        public const string ERROR = "Error";
        public const string UPDATED = "InstallationUpdated/";

        private string _baseUrl;
        readonly ICommandCoordinator _commandCoordinator;

        /// <summary>
        /// Instantiates an instance of <see cref="InstallationCallbackHandler" />
        /// </summary>
        /// <param name="commandCoordinator">The command coordinator needed to issue commands</param>
        public InstallationCallbackHandler(
            ICommandCoordinator commandCoordinator,
            string baseUrl = null
        )
        {
            _commandCoordinator = commandCoordinator;
            _baseUrl = _baseUrl ?? "http://localhost:8080/GitHub/Repositories/";
        }

        /// <inheritdoc />
        public void Install(long installationId, HttpResponse response)
        {
            try {
                var commandResult = _commandCoordinator.Handle(new RegisterInstallation{
                    Id = installationId,
                });

                if (commandResult.Success)
                {
                    RedirectToInstallationFrontendPage(response, SUCCESS+installationId);
                }
                else
                {
                    RedirectToInstallationFrontendPage(response, ERROR);
                }

            } catch (Exception) {
                // TODO: LOG
                RedirectToInstallationFrontendPage(response, ERROR);
            }
        }

        /// <inheritdoc />
        public void Update(long installationId, HttpResponse response)
        {
            // We get the same events through the webhook, but we could show something here?
            RedirectToInstallationFrontendPage(response, UPDATED+installationId);
        }

        void RedirectToInstallationFrontendPage(HttpResponse response, string extra = "")
        {
            response.StatusCode = StatusCodes.Status303SeeOther;
            //TODO: should come from config
            response.Headers["Location"] = _baseUrl + extra;
        }
    }
}