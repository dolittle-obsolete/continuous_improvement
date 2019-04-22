/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Tenancy;
using Domain.Improvements.Metadata;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Continuously watches the pods which are building the improvements for changes
    /// </summary>
    public class BuildPodWatcher
    {
        private readonly FactoryFor<IKubernetes> _clientFactory;
        private readonly ILogger _logger;
        private readonly IExecutionContextManager _executionContextManager;
        private readonly IImprovementMetadataFactory _metadataFactory;
        private readonly IBuildPodProcessor _buildPodProcessor;
        /// <summary>
        /// Instantiates an instance of <see cref="BuildPodWatcher" /> 
        /// </summary>
        /// <param name="logger">A <see cref="ILogger">logger</see></param>
        /// <param name="executionContextManager">An <see cref="IExecutionContextManager"> execution context manager</see></param>
        /// <param name="clientFactory">A factory for creating an <see cref="IKubernetes"> kubernetes client</see></param>
        /// <param name="metadataFactory">A factory for creating <see cref="ImprovementMetadata" /> </param>
        /// <param name="buildPodProcessor">An <see cref="IBuildPodProcessor"> build pod processor</see></param>
        public BuildPodWatcher(
            ILogger logger,
            IExecutionContextManager executionContextManager,
            FactoryFor<IKubernetes> clientFactory,
            IImprovementMetadataFactory metadataFactory,
            IBuildPodProcessor buildPodProcessor
        )
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _executionContextManager = executionContextManager;
            _metadataFactory = metadataFactory;
            _buildPodProcessor = buildPodProcessor;
        }

        /// <summary>
        /// Starts the watcher
        /// </summary>
        public void StartWatcher()
        {
            // FIXME: This should run all the time, so have a look at what happens on exceptions and when it is closed. The client should possibly be disposed.
            Task.Run(async () => {
                var client = _clientFactory();
                var watchList = await client.ListNamespacedPodWithHttpMessagesAsync("dolittle-builds", watch: true);
                watchList.Watch<V1Pod>(
                    // OnEvent
                    (eventType, pod) => {
                        _logger.Trace($"Got event {eventType} for pod '{pod.Metadata.Name}'. The status is {pod.Status.Phase}.");
                        IPod buildPod = null;
                        try
                        {
                            //TODO: create a factory for building the pod
                            buildPod = new Pod(pod,_clientFactory,_metadataFactory);
                            _buildPodProcessor.Process(buildPod);
                        }
                        catch(InvalidImprovementMetadata ex)
                        {
                            _logger.Error(ex,$"Unable to build metadata for '{pod?.Metadata?.Name ?? "[NULL]"}'");
                            DeletePod(pod);
                        }
                    },

                    // OnException
                    (ex) => {
                        _logger.Error(ex, "Error while watching list of build pods.");
                    },

                    // OnClose
                    () => {
                        _logger.Error("Build pod watcher was closed unexpectedly.");
                    }
                );
            });
        }

        //TODO:  centralize pod deletion
        void DeletePod(V1Pod pod) {
            using (var client = _clientFactory())
            {
                client.DeleteNamespacedPod(new V1DeleteOptions {
                    GracePeriodSeconds = 0,
                    PropagationPolicy = "Foreground",
                }, pod.Metadata.Name, pod.Metadata.NamespaceProperty);
            }
        }
    } 
}