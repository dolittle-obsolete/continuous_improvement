
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
* --------------------------------------------------------------------------------------------*/

using System.Linq;
using Concepts.Improvements;
using Domain.Improvements;

namespace Policies.Improvements.Tracking
{
    /// <inheritdoc />
    public class BuildStepsStatusTracker : IBuildStepsStatusTracker
    {
        protected Dictionary<StepNumber, List<StepStatus>> _stepStatuses;

        /// <summary>
        /// Instantiates an instance of <see cref="BuildStepsStatusTracker" />
        /// </summary>
        public BuildStepsStatusTracker()
        {
            _stepStatuses = new Dictionary<StepNumber, List<StepStatus>>();
        }

        /// <inheritdoc />
        public IEnumerator<TrackedStepStatuses> GetEnumerator()
        {
           return  _stepStatuses.Select(_ => new TrackedStepStatuses(_.Key,_.Value)).GetEnumerator();
        }

       /// <inheritdoc />
        public void Track(IContainerStatus containerStepStatus)
        {
            if (_stepStatuses.TryGetValue(containerStepStatus.Step.StepNumber, out var subStepStatuses))
            {
                subStepStatuses.Add(containerStepStatus.Status);
            }
            else
            {
                _stepStatuses.Add(containerStepStatus.Step.StepNumber, new List<StepStatus>(new [] { containerStepStatus.Status } ));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}