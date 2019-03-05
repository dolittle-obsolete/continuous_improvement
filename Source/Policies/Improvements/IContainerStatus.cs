using Concepts.Improvements;
using Dolittle.DependencyInversion;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    public interface IContainerStatus
    {
        StepId Step { get; }
        StepStatus GetStatus();
    }

    public class ContainerStatus : IContainerStatus
    {
        private readonly V1ContainerStatus _status;

        public ContainerStatus(V1ContainerStatus status)
        {
            _status = status;
        }

        public StepId Step => _status.Name;

        public StepStatus GetStatus()
        {
            var exitCode = _status.State.Terminated?.ExitCode;
            if (exitCode.HasValue && exitCode != 0) 
                return StepStatus.Failed;
            if (exitCode.HasValue) 
                return StepStatus.Succeeded;
            if (_status.State.Running != null) 
                return StepStatus.InProgress;

            return StepStatus.NotStarted;
        }
    } 
}