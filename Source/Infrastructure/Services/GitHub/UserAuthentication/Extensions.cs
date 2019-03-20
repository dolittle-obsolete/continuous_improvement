/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.UserAuthentication
{
    /// <summary>
    /// Extensions for the aspnetcore application builder for GitHub user authentication
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds github user authentication
        /// </summary>
        /// <param name="app">The app builder to extend</param>
        /// <param name="path">The route path for the authentication</param>
        public static void UseGitHubUserAuthentication(this IApplicationBuilder app, string path = "thirdparty/github/userauth/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet<Authenticate>(app, path+"initiate");
            routeBuilder.MapGet<Callback>(app, path+"callback");
            routeBuilder.MapGet<GetInstallationsForUserProxy>(app, path+"installations");
            app.UseRouter(routeBuilder.Build());
        }
    }
}