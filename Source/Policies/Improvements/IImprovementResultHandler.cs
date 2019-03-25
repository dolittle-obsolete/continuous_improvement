/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Artifacts;
using Dolittle.Collections;
using Dolittle.Domain;
using Dolittle.Events;
using Dolittle.Execution;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Tenancy;
using Domain.Improvements;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a handler for Improvement Results
    /// </summary>
    public interface IImprovementResultHandler 
    {
        /// <summary>
        /// Handles a successful result for an <see cref="Improvement" />
        /// </summary>
        /// <param name="improvement">The Improvement that succeeded</param>
        void HandleSuccess(ImprovementId improvement);
        /// <summary>
        /// Handles a failed result for an <see cref="Improvement" />
        /// </summary>
        /// <param name="improvement">The Improvement that failed</param>
        void HandleFailure(ImprovementId improvement);
    }
}