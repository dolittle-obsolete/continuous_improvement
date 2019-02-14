/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Dolittle.Execution;

namespace Policies.Improvements
{

    public class ImprovementContextFactory : IImprovementContextFactory
    {
        readonly IExecutionContextManager _executionContextManager;

        public ImprovementContextFactory(IExecutionContextManager executionContextManager)
        {
            _executionContextManager = executionContextManager;
        }

        public ImprovementContext GetFor(
            ImprovableId improvable,
            VersionString version)
        {
            var tenant = _executionContextManager.Current.Tenant;

            return null;
        }
    }
}