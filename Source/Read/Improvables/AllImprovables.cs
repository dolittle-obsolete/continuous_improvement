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

namespace Read.Improvables
{
    /// <summary>
    /// Represents all the <see cref="Project">projects</see>
    /// </summary>
    public class AllImprovables : IQueryFor<Improvable>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AllImprovables"/>
        /// </summary>
        public AllImprovables(ISerializer serializer)
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
            var projectFile = Path.Combine(tenantPath, "improvables.json");
            if (File.Exists(projectFile))
            {
                var json = File.ReadAllText(projectFile);
                Query = serializer.FromJson<IEnumerable<Improvable>>(json).AsQueryable();
            }
            else
            {
                Query = new Improvable[0].AsQueryable();
            }
        }

        /// <summary>
        /// The query that will execute
        /// </summary>
        public IQueryable<Improvable> Query { get; }
    }
}