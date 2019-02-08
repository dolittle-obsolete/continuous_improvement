/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Projects;
using Dolittle.ReadModels;

namespace Read.Projects
{
    /// <summary>
    /// 
    /// </summary>
    public class Step : IReadModel
    {
        /// <summary>
        /// 
        /// </summary>
        public StepType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StepStatus Status { get; set; }
    }
}