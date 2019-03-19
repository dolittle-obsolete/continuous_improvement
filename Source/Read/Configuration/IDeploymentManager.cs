/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;

namespace Read.Configuration
{
    /// <summary>
    /// Defines a manager for a deployment
    /// </summary>
    public interface IDeploymentManager
    {
        /// <summary>
        /// Gets all <see cref="Deployment">Deployments</see>
        /// </summary>
        IEnumerable<Deployment> GetAll();
        /// <summary>
        /// Gets the <see cref="Deployment" /> for the specified id
        /// </summary>
        /// <param name="deployment">The id of the deployment</param>
        Deployment GetById(DeploymentId deployment);
    }
}