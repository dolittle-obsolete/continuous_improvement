/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using Concepts;
using Concepts.Projects;
using Dolittle.Collections;
using Dolittle.Queries;
using Dolittle.Serialization.Json;

namespace Read.Projects
{
    /// <summary>
    /// 
    /// </summary>
    public class StepResultsForStep : IQueryFor<StepResult>
    {
        readonly ISerializer _serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        public StepResultsForStep(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectId Project {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public VersionString Version {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StepNumber Number {  get; set; }

        /// <inheritdoc/>
        public IQueryable<StepResult> Query
        {
            get
            {
                var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
                var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
                var projectPath = Path.Combine(tenantPath, Project.Value.ToString());
                var versionPath = Path.Combine(projectPath, Version);

                var stepsPath = Path.Combine(versionPath, "steps");

                if (Directory.Exists(stepsPath))
                {
                    var stepFilePath = Path.Combine(stepsPath, $"{Number}.json");
                    if (File.Exists(stepFilePath))
                    {
                        var content = File.ReadAllText(stepFilePath);
                        var lines = content.Split('\n');
                        var results = lines.Select(line => _serializer.FromJson<StepResult>(line)).ToArray();
                        return results.AsQueryable();
                    }
                }

                return new StepResult[0].AsQueryable();
            }
        }
    }
}