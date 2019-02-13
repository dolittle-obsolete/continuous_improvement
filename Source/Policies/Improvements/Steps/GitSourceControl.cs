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
    public class GitSourceControl : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("e8529c7a-b6a3-4447-a76b-7aaf32bfdff4");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetFailedEventsFor(ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetSucceededEventsFor(ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}