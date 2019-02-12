/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvements;
using Dolittle.ReadModels;

namespace Read.Improvements
{
    /// <summary>
    /// Represents a step that can be viewed
    /// </summary>
    public class Step : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="StepNumber">number</see> for the step
        /// </summary>
        public StepNumber Number { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="StepType">type</see> of step
        /// </summary>
        public StepType Type { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="StepStatus">status</see> of the step
        /// </summary>
        public StepStatus Status { get; set; }
    }
}