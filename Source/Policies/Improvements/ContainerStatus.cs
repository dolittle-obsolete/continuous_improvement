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

    /// <inheritdoc />
    public class ContainerStatus : IContainerStatus
    {
        private readonly V1ContainerStatus _status;
        /// <summary>
        /// Instantiates an instance of <see cref="ContainerStatus" />
        /// </summary>
        public ContainerStatus(V1ContainerStatus status)
        {
            _status = status;
        }

        /// <inheritdoc />
        public StepId Step => _status.Name;
        /// <inheritdoc />
        public bool IsBuildContainer => Step.IsValid();
        /// <inheritdoc />
        public StepStatus Status => GetStatus();

        StepStatus GetStatus()
        {
            var exitCode = _status.State.Terminated?.ExitCode;
            if (exitCode.HasValue && exitCode != 0) 
                return StepStatus.Failed;
            if (exitCode.HasValue) 
                return StepStatus.Succeeded;
            if (_status.State.Running != null) 
                return StepStatus.InProgress;

            return StepStatus.NotStarted;
        }
    } 
}