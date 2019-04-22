/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Dolittle.Execution;

namespace Policies.Improvements
{
    /// <inheritdoc />
    public class ImprovementContextFactory : IImprovementContextFactory
    {
        readonly IExecutionContextManager _executionContextManager;
        /// <summary>
        /// Instantiates an instance of <see cref="ImprovementContextFactory" />
        /// </summary>
        /// <param name="executionContextManager"></param>
        public ImprovementContextFactory(IExecutionContextManager executionContextManager)
        {
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc />
        public ImprovementContext GetFor(
            ImprovableId improvable,
            Version version)
        {
            var tenant = _executionContextManager.Current.Tenant;

            return null;
        }
    }
}