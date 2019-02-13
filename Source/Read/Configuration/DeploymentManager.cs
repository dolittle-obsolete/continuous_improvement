/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Concepts.Configuration;

namespace Read.Configuration
{
    public class DeploymentManager : IDeploymentManager
    {
        string _tenantPath;

        public DeploymentManager()
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            _tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
        }
        
        public Deployment GetById(DeploymentId deployment)
        {
            throw new System.NotImplementedException();
        }
    }

}