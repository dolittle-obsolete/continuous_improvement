/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Orchestrations.Build
{

    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that is capable of dealing with compilation and packaging
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    public class CompileAndPackage : IPerformer<Context>
    {
        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {
            return true;
        }

        /// <inheritdoc/>
        public async Task Perform(Context context)
        {
            // Cleanup non-active Build jobs
            // If any build jobs are running that we are not tracking - start tracking them

            context.LogInformation("Compiling and packaging");

            var config = new KubernetesClientConfiguration { Host = "http://127.0.0.1:8001" };
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
                                        new V1EnvVar("COMMIT",context.Commit),
                                        new V1EnvVar("PULL_REQUEST", context.IsPullRequest.ToString()),
                                        new V1EnvVar("VERSION", context.Version)
                                    },
                                    VolumeMounts = new[] {
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/repository",
                                            SubPath = context.SourcePath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/packages",
                                            SubPath = context.PackagePath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/output",
                                            SubPath = context.OutputPath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/publish",
                                            SubPath = context.PublishPath
                                        },
                                        new V1VolumeMount {
                                            Name = "azure",
                                            MountPath = "/testresults",
                                            SubPath = context.TestResultsPath
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