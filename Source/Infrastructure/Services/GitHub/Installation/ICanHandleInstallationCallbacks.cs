/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Github.Installation
{
    /// <summary>
    /// Defines a handler for installation callbacks
    /// </summary>
    public interface ICanHandleInstallationCallbacks
    {
        /// <summary>
        /// Handles the callback on installing an installation
        /// </summary>
        /// <param name="installationId">The installation installed</param>
        /// <param name="response">The Http repsonse</param>
        void Install(long installationId, HttpResponse response);

        /// <summary>
        /// Handles the update of the installation
        /// </summary>
        /// <param name="installationId">The installation updated</param>
        /// <param name="response">The HttpResponse</param>
        void Update(long installationId, HttpResponse response);
    }
}