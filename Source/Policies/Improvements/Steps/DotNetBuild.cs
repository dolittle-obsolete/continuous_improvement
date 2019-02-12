/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts.Improvements;
using Dolittle.Events;
using k8s.Models;

namespace Policies.Improvements.Steps
{
    /// <summary>
    /// Represents the step type for dealing with Git source control
    /// </summary>
    public class DotNetBuild : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("602f452f-d7de-4f77-a16b-ee88c85a2921");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> Failed(StepNumber number, ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> Succeeded(StepNumber number, ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}