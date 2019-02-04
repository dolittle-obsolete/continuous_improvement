/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Routing
{
    /// <summary>
    /// Extensions for <see cref="RouteBuilder"/>
    /// </summary>
    public static class RouteBuilderExtensions
    {
        /// <summary>
        /// Map a Get method to a <see cref="ICanHandleRoute"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICanHandleRoute"/> to map to</typeparam>
        /// <param name="routeBuilder"><see cref="RouteBuilder"/> to extend</param>
        /// <param name="application"><see cref="IApplicationBuilder"/> application it is for</param>
        /// <param name="template">Path template</param>
        public static void MapGet<T>(this RouteBuilder routeBuilder, IApplicationBuilder application, string template) where T:ICanHandleRoute
        {
            routeBuilder.MapGet(template, async (request, response, routeData) => {
                var handler = (T)application.ApplicationServices.GetService(typeof(T));
                await handler.Handle(request, response, routeData);
            });
        }

        /// <summary>
        /// Map a Post method to a <see cref="ICanHandleRoute"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICanHandleRoute"/> to map to</typeparam>
        /// <param name="routeBuilder"><see cref="RouteBuilder"/> to extend</param>
        /// <param name="application"><see cref="IApplicationBuilder"/> application it is for</param>
        /// <param name="template">Path template</param>
        public static void MapPost<T>(this RouteBuilder routeBuilder, IApplicationBuilder application, string template) where T:ICanHandleRoute
        {
            routeBuilder.MapPost(template, async (request, response, routeData) => {
                var handler = (T)application.ApplicationServices.GetService(typeof(T));
                await handler.Handle(request, response, routeData);
            });
        }
    }
}