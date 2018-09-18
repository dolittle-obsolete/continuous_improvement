/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EntryPoint
{
    /// <summary>
    /// Defines a handler for routes
    /// </summary>
    public interface ICanHandleRoute
    {
        /// <summary>
        /// Handle a route
        /// </summary>
        /// <param name="request"><see cref="HttpRequest"/> coming in</param>
        /// <param name="response"><see cref="HttpResponse"/> for the request</param>
        /// <param name="routeData">Any <see cref="RouteData"/> associated with the request</param>
        /// <returns><see cref="Task"/> for async operations</returns>
        Task Handle(HttpRequest request, HttpResponse response, RouteData routeData);
    }
}
