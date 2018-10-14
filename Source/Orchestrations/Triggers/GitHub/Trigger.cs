/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Infrastructure.Orchestrations;
using Infrastructure.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Orchestrations;
using Orchestrations.Build;
using Orchestrations.SourceControl;
using Read.Configuration;

namespace Orchestrations.Triggers.GitHub
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
        readonly ILogger _logger;
        private readonly IScoreConfigurator _scoreConfigurator;

        /// <summary>
        /// Initializes a new instance of <see cref="Trigger"/>
        /// </summary>
        /// <param name="serializer">The <see cref="ISerializer"/> to use for deserialization of configuration and payloads</param>
        /// <param name="conductor">The <see cref="IConductor"/> of the score></param>
        /// <param name="logger">The <see cref="ILogger"/> used for logging</param>
        /// <param name="scoreConfigurator">The <see cref="IScoreConfigurator"/> for configuring the score</param>
        public Trigger(
            ISerializer serializer,
            IConductor conductor,
            ILogger logger,
            IScoreConfigurator scoreConfigurator)
        {
            _serializer = serializer;
            _conductor = conductor;
            _logger = logger;
            _scoreConfigurator = scoreConfigurator;
        }

        /// <inheritdoc/>
        public async Task Handle(HttpRequest request, HttpResponse response, RouteData routeData)
        {
            _logger.Information("Handling trigger");

            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            var tenantId = Guid.Parse(routeData.Values[TenantRouteValueName].ToString());
            var projectId = Guid.Parse(routeData.Values[ProjectRouteValueName].ToString());
            var projectPath = Path.Combine(basePath, tenantId.ToString(), projectId.ToString());
            var configurationFile = Path.Combine(projectPath, "configuration.json");

            var projectAsJson = File.ReadAllText(configurationFile);
            var project = _serializer.FromJson<Project>(projectAsJson);


            var @event = request.Headers["X-GitHub-Event"];
            var delivery = request.Headers["X-GitHub-Delivery"];
            var signature = request.Headers["X-Hub-Signature"];
            var isPullRequest = @event.Contains("pull-request");

            var content = new byte[request.ContentLength.Value];
            await request.Body.ReadAsync(content, 0, content.Length);
            var json = Encoding.UTF8.GetString(content);

            var secret = Encoding.UTF8.GetBytes(project.SecretKey);
            var sha1 = GetSHA1(json, secret);
            var expectedSignature = $"sha1={sha1}";
            _logger.Information($"Received signature '{signature}'");
            _logger.Information($"Expected signature '{expectedSignature}");
            if (!signature.Any(_ => _ == expectedSignature))
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var commit = "";
            if( !isPullRequest ) 
            {
                var pushEvent = _serializer.FromJson<PushEvent>(json);
                commit = pushEvent.after;
            }

            var score = _scoreConfigurator.From(tenantId, project, commit, isPullRequest);

            #pragma warning disable 4014 // Don't force await - we want this to run in background and let GitHub continue their business
            Task.Run(() => _conductor.Conduct(score));
            response.StatusCode = StatusCodes.Status200OK;

            await Task.CompletedTask;
        }

        int GetBuildNumberForCurrentBuild(string projectPath)
        {
            var buildNumberFile = Path.Combine(projectPath, "buildNumber");
            var buildNumber = 1;
            
            if( File.Exists(buildNumberFile)) 
            {
                var buildNumberAsText = File.ReadAllText(buildNumberFile);
                buildNumber = int.Parse(buildNumberAsText) + 1;
            }
            File.WriteAllText(buildNumberFile, buildNumber.ToString());
            return buildNumber;
        }

        string GetSHA1(string input, byte[] key)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            using(var myhmacsha1 = new HMACSHA1(key))
            {
                var hashArray = myhmacsha1.ComputeHash(byteArray);
                return hashArray.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            }
        }
    }
}