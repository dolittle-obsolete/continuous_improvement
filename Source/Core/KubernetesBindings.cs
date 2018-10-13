/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.DependencyInversion;
using k8s;

namespace Core
{
    /// <summary>
    /// Represents bindings for the Kubernetes client
    /// </summary>
    public class KubernetesBindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var localApi = "http://127.0.0.1:8001";
            var kubernetesApi = Environment.GetEnvironmentVariable("KUBERNETES_API") ?? localApi;

            var kubernetesTokenPath = "/var/run/secrets/kubernetes.io/serviceaccount/token";

            var config = new KubernetesClientConfiguration 
            { 
                Host = kubernetesApi,
                SkipTlsVerify = true
            };
            if( kubernetesApi != localApi && File.Exists(kubernetesTokenPath))
            {
                config.AccessToken = File.ReadAllText(kubernetesTokenPath);
            }
            var client = new Kubernetes(config);
            builder.Bind<Kubernetes>().To(client).Singleton();
        }
    }
}