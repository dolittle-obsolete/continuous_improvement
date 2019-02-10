/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using Concepts;
using Concepts.Projects;
using Dolittle.Queries;

namespace Read.Projects
{
    /// <summary>
    /// Represents a query for getting a <see cref="StepRawLog"/> for a <see cref="Step"/>
    /// </summary>
    public class RawLogForStep : IQueryFor<StepRawLog>
    {

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
        public IQueryable<StepRawLog> Query
        {
            get
            {
                var log = new StepRawLog();

                var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
                var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
                var projectPath = Path.Combine(tenantPath, Project.Value.ToString());
                var versionPath = Path.Combine(projectPath, Version);

                var stepsPath = Path.Combine(versionPath, "steps");

                if (Directory.Exists(stepsPath))
                {
                    var stepFilePath = Path.Combine(stepsPath,$"{Number}.log");
                    if( File.Exists(stepFilePath)) 
                    {
                        log.Content = File.ReadAllText(stepFilePath);
                    }
                }

                return new[] { log }.AsQueryable();
            }
        }
    }
}