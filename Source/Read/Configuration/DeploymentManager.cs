/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Configuration;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Serialization.Json;

namespace Read.Configuration
{
    public class DeploymentManager : IDeploymentManager
    {
        const string _deploymentsFile = "deployments.json";
        private readonly ISerializer _serializer;
        private readonly IFiles _fileSystem;

        public DeploymentManager(IFiles fileSystem, ISerializer serializer)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;
        }

        public IEnumerable<Deployment> GetAll()
        {
            if( !_fileSystem.Exists(_deploymentsFile)) return new Deployment[0];
            var json = _fileSystem.ReadAllText(_deploymentsFile);
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