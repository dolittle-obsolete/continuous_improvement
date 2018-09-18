/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Serialization.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Orchestrations;
using Orchestrations.Build;
using Orchestrations.SourceControl;
using Read.Configuration;

namespace EntryPoint.GitHub
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

            var tenantId = Guid.Parse(routeData.Values[TenantRouteValueName].ToString());
            var projectId = Guid.Parse(routeData.Values[ProjectRouteValueName].ToString());
            var basePath = Path.Combine(Directory.GetCurrentDirectory(),"Builds",tenantId.ToString(), projectId.ToString());

            var content = new byte[request.ContentLength.Value];
            await request.Body.ReadAsync(content, 0, content.Length);
            var json = Encoding.UTF8.GetString(content);

            File.WriteAllText("payload.json",json);
            var payload = _serializer.FromJson<Payload>(json);

            var configurationFile = Path.Combine(basePath,"configuration.json");

            var projectAsJson = File.ReadAllText(configurationFile);
            var project = _serializer.FromJson<Project>(projectAsJson);
            var buildNumberFile = Path.Combine(basePath,"buildNumber");
            var buildNumberAsText = File.ReadAllText(buildNumberFile);
            var buildNumber = int.Parse(buildNumberAsText)+1;
            File.WriteAllText(buildNumberFile, buildNumber.ToString());

            var source = Path.Combine(Directory.GetCurrentDirectory(),"Builds","508c1745-5f2a-4b4c-b7a5-2fbb1484346d","f1d0d79d-d47e-4b56-9663-d8c11fe3a9f4","source");

            var isPullRequest = payload.pull_request != null;
            var context = new Context(project, source, "beb7544a44dff9283ba2f1d5c3cc8a567dfffa6c", buildNumber, false);
            var score = new ScoreOf<Context>(context);
            score.AddStep(new GetLatest());
            score.AddStep(new GetVersion());
            score.AddStep(new CompileAndPackage());

            _conductor.Conduct(score);

            response.StatusCode = StatusCodes.Status200OK;
            await Task.CompletedTask;
        }
    }
}