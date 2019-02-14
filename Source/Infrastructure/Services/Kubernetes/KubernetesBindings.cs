/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.DependencyInversion;
using k8s;

namespace Infrastructure.Services.Kubernetes
{
    /// <summary>
    /// Represents bindings for the Kubernetes client
    /// </summary>
    public class KubernetesBindings : ICanProvideBindings
    {
        const string _localApi = "http://127.0.0.1:8001";
        const string _clusterApiEnvName = "KUBERNETES_API";
        const string _clusterTokenPath = "/var/run/secrets/kubernetes.io/serviceaccount/token";


        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var config = new KubernetesClientConfiguration();

            if (Environment.GetEnvironmentVariable(_clusterApiEnvName) != null && File.Exists(_clusterTokenPath))
            {
                // Use config for an in-cluster client
                config.Host = Environment.GetEnvironmentVariable(_clusterApiEnvName);
                config.AccessToken = File.ReadAllText(_clusterTokenPath);
                config.SkipTlsVerify = false;
            }
            else
            {
                // Allow connecting through a local tunnel during development
                config.Host = _localApi;
                config.SkipTlsVerify = true;
            }

            builder.Bind<IKubernetes>().To(() => new k8s.Kubernetes(config));
        }
    }
}