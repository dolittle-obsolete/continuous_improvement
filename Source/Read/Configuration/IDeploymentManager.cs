/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;

namespace Read.Configuration
{
    public interface IDeploymentManager
    {
        IEnumerable<Deployment> GetAll();
        Deployment GetById(DeploymentId deployment);
    }

}