/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.SourceControl.GitHub;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Extension methods for the Application Aggregate Root Repository
    /// </summary>
    public static class ApplicationRepositoryExtensions
    {
        static readonly EventSourceId SINGLETON_ID = new Guid("ff042dd1-d012-46cb-b5ed-867ba80d54e3");

        /// <summary>
        /// Gets the single instance of the <see cref="Application" />
        /// </summary>
        /// <param name="repository">The instance of the <see cref="IAggregateRootRepositoryFor{Application}" /> being extended</param>
        public static Application GetApplication(this IAggregateRootRepositoryFor<Application> repository)
        {
            return repository.Get(SINGLETON_ID);
        }
    }
}
