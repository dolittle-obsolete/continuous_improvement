/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Concepts;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using Infrastructure.Orchestrations;
using Orchestrations.Build;
using Orchestrations.SourceControl;
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents an implementation of <see cref="IScoreConfigurator"/>
    /// </summary>
    public class ScoreConfigurator : IScoreConfigurator
    {
        readonly ISerializer _serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        public ScoreConfigurator(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public ScoreOf<Context> From(TenantId tenantId, Project project, string commit, bool isPullRequest)
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            var projectPath = Path.Combine(basePath, tenantId.ToString(), project.Id.ToString());
            var buildNumber = GetBuildNumberForCurrentBuild(projectPath);

            var sourceControl = new SourceControlContext(project.Repository, commit, isPullRequest);
            var context = new Context(tenantId, project, sourceControl, projectPath, buildNumber);
            var score = new ScoreOf<Context>(context);
            score.AddStep<GetLatest>();
            score.AddStep<GetVersion>();
            score.AddStep<BuildJobs>();

            return score;
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
    }
}