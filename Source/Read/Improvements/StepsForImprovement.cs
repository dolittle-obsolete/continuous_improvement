/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts;
using Dolittle.Collections;
using Dolittle.Queries;
using Dolittle.Serialization.Json;

namespace Read.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class StepsForImprovement : IQueryFor<Step>
    {
        readonly ISerializer _serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer">Json Serializer</see></param>
        public StepsForImprovement(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectId Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public VersionString Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Step> Query
        {
            get 
            {
                var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
                var tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
                var projectPath = Path.Combine(tenantPath, Project.Value.ToString());
                var versionPath = Path.Combine(projectPath,Version);
                var stepsFile = Path.Combine(versionPath,"steps.json");
                var stepsAsJson = File.ReadAllText(stepsFile);
                var steps = _serializer.FromJson<IEnumerable<Step>>(stepsAsJson).ToArray();

                return steps.AsQueryable();
            }
        }
    }
}