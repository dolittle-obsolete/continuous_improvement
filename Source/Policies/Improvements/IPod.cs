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
    /// <summary>
    /// Defines the interface for a Pod
    /// </summary>
    public interface IPod
    {
        /// <summary>
        /// Indicates whether or not the pod is deleted
        /// </summary>
        bool IsDeleted { get; }
        /// <summary>
        /// Indicates whether or not the pod has succeeded
        /// </summary>
        bool HasSucceeded { get; }
        /// <summary>
        /// Indicates whether or not the pod has failed
        /// </summary>
        bool HasFailed { get; }
        /// <summary>
        /// The status of the pod
        /// </summary>
        string Status { get; }
        /// <summary>
        /// Indicates whether or not the pod has container statuses
        /// </summary>
        bool HasBuildContainerStatuses { get; }
        /// <summary>
        /// A list of container statuses
        /// </summary>
        IEnumerable<IContainerStatus> Statuses { get; }
        /// <summary>
        /// The improvement metadata
        /// </summary>
        ImprovementMetadata Metadata { get; }
        /// <summary>
        /// Deletes the pod
        /// </summary>
        void Delete();
    }

    /// <inheritdoc />
    public class Pod : IPod
    {
        /// <summary>
        /// String constant for Succeeded
        /// </summary>
        public const string SUCCEEDED = "Succeeded";
        /// <summary>
        /// String constant for Failed
        /// </summary>
        public const string FAILED = "Failed";
        /// <summary>
        /// String constant for Unknown
        /// </summary>
        public const string UNKNOWN = "Unknown";

        private readonly V1Pod _pod;
        private readonly FactoryFor<IKubernetes> _clientFactory;

        /// <summary>
        /// Instantiates an instance of <see cref="Pod"/>
        /// </summary>
        /// <param name="pod">The kubernetes pod</param>
        /// <param name="clientFactory">A factory for creating the <see cref="IKubernetes">kubernetes client</see></param>
        /// <param name="metadataFactory">A factory for creating <see cref="ImprovementMetadata" /></param>
        public Pod(V1Pod pod, FactoryFor<IKubernetes> clientFactory, IImprovementMetadataFactory metadataFactory)
        {
            _pod = pod;
            _clientFactory = clientFactory;
            Metadata = metadataFactory.BuildFrom(pod.Metadata.Labels,pod.Metadata.Name);
        }
        /// <inheritdoc />
        public bool IsDeleted => _pod?.Metadata?.DeletionTimestamp.HasValue ?? false;
        /// <inheritdoc />
        public bool HasSucceeded => _pod?.Status?.Phase == SUCCEEDED;
        /// <inheritdoc />
        public bool HasFailed => _pod?.Status?.Phase == FAILED;
        /// <inheritdoc />
        public string Status => _pod?.Status?.Phase ?? UNKNOWN;
        /// <inheritdoc />
        public bool HasBuildContainerStatuses => Statuses.Any();
        /// <inheritdoc />
        public IEnumerable<IContainerStatus> Statuses => _pod?.Status?.InitContainerStatuses.Select(_ => new ContainerStatus(_)).Where(s => s.IsBuildContainer).ToList();
        /// <inheritdoc />
        public ImprovementMetadata Metadata { get; }
        /// <inheritdoc />
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