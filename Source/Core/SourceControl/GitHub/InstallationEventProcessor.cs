using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.SourceControl.GitHub;
using System.Linq;
using Dolittle.Execution;
using System.IO;
using Infrastructure.Services.Github.Webhooks.Handling;

namespace Core.SourceControl.GitHub
{
    public class InstallationEventProcessor : ICanProcessEvents
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly IInstallationToTenantMapper _tenantMapper;

        public InstallationEventProcessor(IExecutionContextManager executionContextManager, IInstallationToTenantMapper tenantMapper)
        {
            _executionContextManager = executionContextManager;
            _tenantMapper = tenantMapper;
        }
        
        [EventProcessor("27ff986e-1caf-40e6-98c1-b9ee67ea6dfd")]
        public void Process(InstallationRegistered @event)
        {
            var tenantId = _executionContextManager.Current.Tenant;
            var installationId = new Octokit.InstallationId(@event.InstallationId);

            _tenantMapper.SetTenantFor(installationId, tenantId);
        }

        [EventProcessor("40c28d57-fa35-48ed-bac8-40e972e17649")]
        public void Process(InstallationUnregistered @event)
        {
            var installationId = new Octokit.InstallationId(@event.InstallationId);
            _tenantMapper.UnsetTenantFor(installationId);
        }
    }
}
