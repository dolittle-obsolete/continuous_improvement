/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Serialization.Json;

namespace Read.Projects
{
    /// <summary>
    /// Represents all the <see cref="Project">projects</see>
    /// </summary>
    public class AllProjects : IQueryFor<Project>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AllProjects"/>
        /// </summary>
        public AllProjects(ISerializer serializer)
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
            var projectFile = Path.Combine(tenantPath, "projects.json");
            if (File.Exists(projectFile))
            {
                var json = File.ReadAllText(projectFile);
                Query = serializer.FromJson<IEnumerable<Project>>(json).AsQueryable();
            }
            else
            {
                Query = new Project[0].AsQueryable();
            }
        }

        /// <summary>
        /// The query that will execute
        /// </summary>
        public IQueryable<Project> Query { get; }
    }
}