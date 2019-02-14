/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Configuration;
using Dolittle.Serialization.Json;

namespace Read.Configuration
{
    public class DeploymentManager : IDeploymentManager
    {
        string _tenantPath;
        private readonly ISerializer _serializer;

        public DeploymentManager(ISerializer serializer)
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            _tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
            _serializer = serializer;
        }

        public IEnumerable<Deployment> GetAll()
        {
            var deploymentsPath = Path.Combine(_tenantPath,"deployments.json");
            if( !File.Exists(deploymentsPath)) return new Deployment[0];
            var json = File.ReadAllText(deploymentsPath);
            var deployments = _serializer.FromJson<IEnumerable<Deployment>>(json);
            return deployments;
        }
        
        public Deployment GetById(DeploymentId deploymentId)
        {
            var deployment = GetAll().Single(_ => _.Id == deploymentId);
            return deployment;
        }
    }
}