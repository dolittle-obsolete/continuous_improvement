/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Infrastructure.Routing;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Orchestrations.Build
{
    /// <summary>
    /// Represents a <see cref="ICanHandleRoute"/> that gets called when a <see cref="BuildJobs"/> is finished
    /// </summary>
    public class BuildJobDone : ICanHandleRoute
    {
        readonly ILogger _logger;
        private readonly Kubernetes _kubernetes;

        /// <summary>
        /// Initializes a new instance of <see cref="BuildJobDone"/>
        /// </summary>
        /// <param name="kubernetes"><see cref="Kubernetes"/> client</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public BuildJobDone(Kubernetes kubernetes, ILogger logger)
        {
            _logger = logger;
            _kubernetes = kubernetes;
        }


        /// <inheritdoc/>
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            var jobName = request.Query["jobName"];
            _logger.Information($"Job '{jobName}' is done - deleting it");

            var @namespace = "dolittle";
            var deleteOptions = new V1DeleteOptions();

            await _kubernetes.DeleteNamespacedJobAsync(deleteOptions, jobName, @namespace);
            await _kubernetes.DeleteNamespacedPodAsync(deleteOptions, jobName, @namespace);
        }
    }
}