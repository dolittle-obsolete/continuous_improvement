/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Concepts.Improvements;
using Dolittle.DependencyInversion;
using Domain.Improvements.Metadata;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    public interface IPod
    {
        bool IsDeleted { get; }
        bool HasSucceeded { get; }
        bool HasFailed { get; }
        string Status { get; }
        bool HasBuildContainerStatuses { get; }
        IEnumerable<IContainerStatus> Statuses { get; }
        ImprovementMetadata Metadata { get; }
        void Delete();
    }

    public class Pod : IPod
    {
        public const string SUCCEEDED = "Succeeded";
        public const string FAILED = "Failed";
        public const string UNKNOWN = "Unknown";

        private readonly V1Pod _pod;
        private readonly FactoryFor<IKubernetes> _clientFactory;

        public Pod(V1Pod pod, FactoryFor<IKubernetes> clientFactory, IImprovementMetadataFactory metadataFactory)
        {
            _pod = pod;
            _clientFactory = clientFactory;
            Metadata = metadataFactory.BuildFrom(pod.Metadata.Labels,pod.Metadata.Name);
        }
        public bool IsDeleted => _pod?.Metadata?.DeletionTimestamp.HasValue ?? false;

        public bool HasSucceeded => _pod?.Status?.Phase == SUCCEEDED;

        public bool HasFailed => _pod?.Status?.Phase == FAILED;

        public string Status => _pod?.Status?.Phase ?? UNKNOWN;

        public bool HasBuildContainerStatuses => Statuses.Any();

        public IEnumerable<IContainerStatus> Statuses => _pod?.Status?.InitContainerStatuses.Select(_ => new ContainerStatus(_)).Where(s => s.IsBuildContainer).ToList();

        public ImprovementMetadata Metadata { get; }

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