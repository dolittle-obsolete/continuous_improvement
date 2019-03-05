using Concepts.Improvements;
using Dolittle.DependencyInversion;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    public interface IPod
    {
        bool IsDeleted { get; }
    }

    public class Pod : IPod
    {
        public const string SUCCEEDED = "Succeeded";
        public const string FAILED = "Failed";
        public const string UNKNOWN = "Unknown";

        private readonly V1Pod _pod;
        private readonly FactoryFor<IKubernetes> _clientFactory;

        public Pod(V1Pod pod, FactoryFor<IKubernetes> clientFactory)
        {
            _pod = pod;
            _clientFactory = clientFactory;
        }
        public bool IsDeleted => _pod?.Metadata?.DeletionTimestamp.HasValue ?? false;

        public bool HasSucceeded => _pod?.Status?.Phase == SUCCEEDED;

        public bool HasFailed => _pod?.Status?.Phase == FAILED;

        public string Status => _pod?.Status?.Phase ?? UNKNOWN;

        public string Metadata => _pod?.Metadata?.ToString() ?? string.Empty; //Not sure about this...

        public bool HasStatuses => _pod?.Status?.InitContainerStatuses != null;

        public void Delete()
        {
            using (var client = _clientFactory())
            {
                client.DeleteNamespacedPod(new V1DeleteOptions {
                    GracePeriodSeconds = 0,
                    PropagationPolicy = "Foreground",
                }, _pod.Metadata.Name, _pod.Metadata.NamespaceProperty);
            }
        }
    }
}