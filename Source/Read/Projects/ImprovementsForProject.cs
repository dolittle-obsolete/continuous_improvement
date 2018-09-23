/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using Concepts;
using Dolittle.Queries;

namespace Read.Projects
{
    /// <summary>
    /// 
    /// </summary>
    public class ImprovementsForProject : IQueryFor<Improvement>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectId Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Improvement> Query 
        { 
            get
            {
                var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
                var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
                var projectPath = Path.Combine(tenantPath, Project.Value.ToString());
                var builds = Directory.GetDirectories(projectPath);
                return builds.Select(_ => 
                {
                    var segments = _.Split(Path.DirectorySeparatorChar);
                    return new Improvement { Version = segments[segments.Length-1] };
                }).AsQueryable();
            }
        }
        
    }
}