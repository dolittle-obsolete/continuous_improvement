/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Events.Processing;
using Dolittle.Execution;
using Dolittle.ReadModels;
using Events.SourceControl.GitHub;
using Infrastructure.Services.Github.Webhooks.Handling;

namespace Core.SourceControl.GitHub
{
    /// <summary>
    /// An event processor for Installation Events
    /// </summary>
    public class InstallationEventProcessor : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IInstallationToTenantMapper _tenantMapper;

        /// <summary>
        /// Instantiates an instanace of <see cref="InstallationEventProcessor" />
        /// </summary>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" /> to set the correct <see cref="ExecutionContext" /></param>
        /// <param name="tenantMapper">The <see cref="IInstallationToTenantMapper" /> to map <see cref="Installation">Installations</see> to <see cref="Tenant">Tenants></see></param>
        public InstallationEventProcessor(IExecutionContextManager executionContextManager, IInstallationToTenantMapper tenantMapper)
        {
            _executionContextManager = executionContextManager;
            _tenantMapper = tenantMapper;
        }

        /// <summary>
        /// Processes the <see cref="InstallationRegistered" /> event
        /// </summary>
        [EventProcessor("27ff986e-1caf-40e6-98c1-b9ee67ea6dfd")]
        public void Process(InstallationRegistered @event)
        {
            var tenantId = _executionContextManager.Current.Tenant;
            var installationId = new Octokit.InstallationId(@event.InstallationId);

            _tenantMapper.AssociateTenantWithInstallation(installationId.Id, tenantId);
        }

        /// <summary>
        /// Processes the <see cref="InstallationUnregistered" /> event
        /// </summary>
        [EventProcessor("40c28d57-fa35-48ed-bac8-40e972e17649")]
        public void Process(InstallationUnregistered @event)
        {
            var installationId = new Octokit.InstallationId(@event.InstallationId);
            _tenantMapper.DisassociateTenantFromInstallation(installationId.Id);
        }
    }
}