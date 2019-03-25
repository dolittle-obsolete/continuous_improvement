/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvements;
using Dolittle.DependencyInversion;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines the contract for a ContainerStatus
    /// </summary>
    public interface IContainerStatus
    {
        /// <summary>
        /// Indicates whether the Container is a Build Container or not
        /// </summary>
        bool IsBuildContainer { get; }
        /// <summary>
        /// The Id of the Step
        /// </summary>
        StepId Step { get; }
        /// <summary>
        /// The status of the Step
        /// </summary>
        StepStatus Status { get; }
    }
}