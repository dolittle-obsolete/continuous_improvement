/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Orchestrations;
using k8s;
using k8s.Models;
using Dolittle.Logging;

namespace Orchestrations.Build
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that is capable of dealing with compilation and packaging
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    public class CompileAndPackage : IPerformer<Context>
    {
        readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CompileAndPackage(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {
            return true;
        }

        /// <inheritdoc/>
        public async Task Perform(Context context)
        {
            context.LogInformation("Compiling and packaging");

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

            _logger.Information($"Using Kubernetes API @ '{kubernetesApi}'");
            _logger.Information($"Using Access Token '{config.AccessToken}'");
            
            var client = new Kubernetes(config);

            var @namespace = "dolittle";
            var metadata = new V1ObjectMeta
            {
                Name = Guid.NewGuid().ToString(),
            };

            var job = new V1Job
            {
                Metadata = metadata,
                Spec = new V1JobSpec
                {
                    Completions = 1,

                    Template = new V1PodTemplateSpec
                    {
                        Metadata = metadata,
                        Spec = new V1PodSpec 
                        {
                            Containers = new [] {
                                new V1Container {
                                    Name = "build",
                                    Image = $"dolittlebuild/{context.Project.Type}",
                                    ImagePullPolicy = "IfNotPresent",
                                    Env = new [] {
                                        new V1EnvVar("REPOSITORY",context.Project.Repository.ToString()),
                                        new V1EnvVar("COMMIT",context.SourceControl.Commit),
                                        new V1EnvVar("PULL_REQUEST", context.IsPullRequest.ToString()),
                                        new V1EnvVar("VERSION", context.Version)
                                    },
                                    VolumeMounts = new[] {
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/repository",
                                            SubPath = context.Volumes.SourcePath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/packages",
                                            SubPath = context.Volumes.PackagePath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/output",
                                            SubPath = context.Volumes.OutputPath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/publish",
                                            SubPath = context.Volumes.PublishPath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/testresults",
                                            SubPath = context.Volumes.TestResultsPath
                                        }
                                    }
                                }
                            },
                            Volumes = new[] {
                                new V1Volume {
                                    Name = "azure",
                                    AzureFile = new V1AzureFileVolumeSource {
                                        SecretName = "azure-storage-secret",
                                        ShareName = "continuousimprovement",
                                        ReadOnlyProperty = false
                                    }
                                }
                            },
                            RestartPolicy = "Never",                            
                        }
                    }
                }
            };

            await Task.Run(async () => {
                var status = await client.CreateNamespacedJobAsync(job, @namespace);
                for(;;) 
                {
                    Thread.Sleep(500);
                    status = await client.ReadNamespacedJobStatusAsync(metadata.Name, @namespace);
                    if( (status.Status.Active ?? 0) == 0 ) 
                    {
                        // Cleanup
                        break;
                    }
                }
            });
        }
    }
}