/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Configuration;

namespace Read.Configuration
{
    public interface IDeploymentManager
    {
        Deployment GetById(DeploymentId deployment);
    }

}