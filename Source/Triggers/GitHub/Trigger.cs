/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Serialization.Json;
using Infrastructure.Orchestrations;
using Infrastructure.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Orchestrations;
using Orchestrations.Build;
using Orchestrations.SourceControl;
using Read.Configuration;

namespace Triggers.GitHub
{
    /// <summary>
    /// Represents a handler for triggers coming from GitHub
    /// </summary>
    public class Trigger : ICanHandleRoute
    {
        /// <summary>
        /// Gets the route value name holding the tenant
        /// </summary>
        public const string TenantRouteValueName = "tenant";

        /// <summary>
        /// Gets the route value name holding the project
        /// </summary>
        public const string ProjectRouteValueName = "project";
        readonly ISerializer _serializer;
        readonly IConductor _conductor;

        /// <summary>
        /// Initializes a new instance of <see cref="Trigger"/>
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="conductor"></param>
        public Trigger(ISerializer serializer, IConductor conductor)
        {
            _serializer = serializer;
            _conductor = conductor;
        }

        /// <inheritdoc/>
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            var @event = request.Headers["X-GitHub-Event"];
            var delivery = request.Headers["X-GitHub-Delivery"];
            var signature = request.Headers["X-Hub-Signature"];

            var isPullRequest = @event.Contains("pull-request");

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Builds");
            basePath = "/Volumes/continuousimprovement";

            var tenantId = Guid.Parse(routeData.Values[TenantRouteValueName].ToString());
            var projectId = Guid.Parse(routeData.Values[ProjectRouteValueName].ToString());

            var projectPath = Path.Combine(basePath, tenantId.ToString(), projectId.ToString());

            var content = new byte[request.ContentLength.Value];
            await request.Body.ReadAsync(content, 0, content.Length);
            var json = Encoding.UTF8.GetString(content);

            //File.WriteAllText("payload.json",json);
            //var payload = _serializer.FromJson<Payload>(json);

            var configurationFile = Path.Combine(projectPath, "configuration.json");

            var projectAsJson = File.ReadAllText(configurationFile);
            var project = _serializer.FromJson<Project>(projectAsJson);
            int buildNumber = GetBuildNumberForCurrentBuild(projectPath);

            var sourceControl = new SourceControlContext(project.Repository,"beb7544a44dff9283ba2f1d5c3cc8a567dfffa6c", isPullRequest);
           
            var context = new Context(tenantId, project, sourceControl, basePath, buildNumber);
            var score = new ScoreOf<Context>(context);
            score.AddStep<GetLatest>();
            score.AddStep<GetVersion>();
            score.AddStep<CompileAndPackage>();

            _conductor.Conduct(score);

            response.StatusCode = StatusCodes.Status200OK;
            await Task.CompletedTask;
        }

        int GetBuildNumberForCurrentBuild(string projectPath)
        {
            var buildNumberFile = Path.Combine(projectPath, "buildNumber");
            var buildNumberAsText = File.ReadAllText(buildNumberFile);
            var buildNumber = int.Parse(buildNumberAsText) + 1;
            File.WriteAllText(buildNumberFile, buildNumber.ToString());
            return buildNumber;
        }
    }
}