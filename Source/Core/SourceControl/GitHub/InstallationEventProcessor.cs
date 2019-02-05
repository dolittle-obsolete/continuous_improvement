using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Events.Processing;
using Dolittle.ReadModels;
using Events.SourceControl.GitHub;
using System.Linq;
using Dolittle.Execution;
using System.IO;

namespace Core.SourceControl.GitHub
{
    public class InstallationEventProcessor : ICanProcessEvents
    {
        IExecutionContextManager _executionContextManager;
        public InstallationEventProcessor(IExecutionContextManager executionContextManager)
        {
            _executionContextManager = executionContextManager;
        }
        
        [EventProcessor("27ff986e-1caf-40e6-98c1-b9ee67ea6dfd")]
        public void Process(InstallationRegistered @event)
        {
            var installationTenantId = _executionContextManager.Current.Tenant;
            var installationGithubId = @event.InstallationId;

            // Save the mapping
            using (var file = new StreamWriter("./webhooks-mapping.txt", true))
            {
                file.Write(installationGithubId);
                file.Write(" -> ");
                file.WriteLine(installationTenantId);
            }
        }
        
    }
}
