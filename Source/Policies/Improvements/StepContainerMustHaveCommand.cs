/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Exception that gets thrown when no Command is explicitly set on a <see cref="V1Container">container</see> used as part of a <see cref="IStep">build step</see>
    /// </summary>
    public class StepContainerMustHaveCommand : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="StepContainerMustHaveCommand"/>
        /// </summary>
        /// <returns></returns>
        public StepContainerMustHaveCommand() : base("The container definition needs to have the command set explicitly for the log handler to work.")
        {}
    }
}