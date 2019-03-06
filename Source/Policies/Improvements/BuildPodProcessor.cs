using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Policies.Improvements.StepHandling;
using Policies.Improvements.Tracking;

namespace Policies.Improvements
{
    public interface IBuildPodProcessor
    {
        void Process(IPod pod);
    }

    public class BuildPodProcessor : IBuildPodProcessor
    {
        private readonly ILogger _logger;
        private readonly IHandleBuildSteps _handleBuildSteps;
        private readonly FactoryFor<IBuildStepsStatusTracker> _getTracker;
        private readonly IExecutionContextManager _executionContextManager;

        public BuildPodProcessor(IExecutionContextManager executionContextManager, IHandleBuildSteps handleBuildSteps, FactoryFor<IBuildStepsStatusTracker> getTracker, ILogger logger)
        {
            _logger = logger;
            _handleBuildSteps = handleBuildSteps;
            _getTracker = getTracker;
            _executionContextManager = executionContextManager;
        }

        public void Process(IPod pod)
        {  
            _executionContextManager.CurrentFor(pod.Metadata.Tenant); 
            if(pod.IsDeleted)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' is deleted.");
                return;
            } 

            if(!pod.HasStatuses)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' has no statuses to process.");
                return;
            } 

            if (pod.HasSucceeded)
            {
                _logger.Information($"Build-pod '{pod.Metadata.ToString()}' succeeded.");
                ProcessSteps(pod);
                pod.Delete();
                return;
            }
            else if (pod.HasFailed)
            {
                _logger.Warning($"Build-pod '{pod.Metadata.ToString()}' failed.");
                pod.Delete();
                return;
            }
            _logger.Information($"Build-pod '{pod.Metadata.ToString()}' still in progress.");
            ProcessSteps(pod);
        }

        void ProcessSteps(IPod pod)
        {
            var tracker = _getTracker();
            pod.Statuses.ForEach(_ => tracker.Track(_.Step.StepNumber,_.GetStatus()));
            _handleBuildSteps.Handle(pod.Metadata,tracker);
        }
    }
}