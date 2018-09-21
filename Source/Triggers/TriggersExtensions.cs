/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Triggers
{
    /// <summary>
    /// Holds extension methods to work with setting up triggers
    /// </summary>
    public static class TriggersExtensions
    {
        /// <summary>
        /// Use GitHub for triggering builds
        /// </summary>
        /// <param name="app"></param>
        public static void UseGitHubTrigger(this IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapPost<GitHub.Trigger>(app,$"triggers/github/{{{GitHub.Trigger.TenantRouteValueName}:guid}}/{{{GitHub.Trigger.ProjectRouteValueName}:guid}}");
            app.UseRouter(routeBuilder.Build());
        }
    }
}