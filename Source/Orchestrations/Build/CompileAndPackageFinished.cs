/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Orchestrations.Build
{
    /// <summary>
    /// Represents a <see cref="ICanHandleRoute"/> that gets called when a <see cref="CompileAndPackage"/> is finished
    /// </summary>
    public class CompileAndPackageFinished : ICanHandleRoute
    {
        /// <inheritdoc/>
        public Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            return Task.CompletedTask;
        }
    }
}