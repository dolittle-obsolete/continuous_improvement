/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.Webhooks
{
    /// <summary>
    /// Extension methods for registering the webhook handler with the aspnetcore application builder
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Tells the application builder to use the github webhook handler
        /// </summary>
        /// <param name="app">The application builder being extended</param>
        /// <param name="path">The route path for the github webhook handler</param>
        public static void UseGitHubWebhookHandler(this IApplicationBuilder app, string path = "thirdparty/github/webhooks/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapPost<Route>(app, path);
            app.UseRouter(routeBuilder.Build());
        }
    }
}