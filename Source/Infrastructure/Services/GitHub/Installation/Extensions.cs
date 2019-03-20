/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.Installation
{
    /// <summary>
    /// Extensions for the aspnet core app builder for adding a Github installation handler
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds a github installation handler to the application
        /// </summary>
        /// <param name="app">The application being extended</param>
        /// <param name="path">The route of the handler</param>
        public static void UseGitHubInstallationHandler(this IApplicationBuilder app, string path = "thirdparty/github/installation/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet<Setup>(app, path+"setup");
            app.UseRouter(routeBuilder.Build());
        }
    }
}