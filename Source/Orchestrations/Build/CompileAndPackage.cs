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
    public class CompileAndPackage : IPerformer<Context>
    {
        readonly ILogger _logger;
        readonly Kubernetes _kubernetes;

        /// <summary>
        /// Initializes a new instance of <see cref="CompileAndPackage"/>
        /// </summary>
        /// <param name="kubernetes"><see cref="Kubernetes"/> client</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public CompileAndPackage(
            Kubernetes kubernetes,
            ILogger logger)
        {
            _kubernetes = kubernetes;
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

            var @namespace = "dolittle";
            var metadata = new V1ObjectMeta
            {
                Name = Guid.NewGuid().ToString(),
                Labels = { { "type", "build" } }
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
                                    ImagePullPolicy = "Always",
                                    Env = new [] {
                                        new V1EnvVar("REPOSITORY",context.Project.Repository.ToString()),
                                        new V1EnvVar("COMMIT",context.SourceControl.Commit),
                                        new V1EnvVar("PULL_REQUEST", context.IsPullRequest.ToString()),
                                        new V1EnvVar("VERSION", context.Version),
                                        new V1EnvVar("CALLBACK", $"http://continuousimprovement/jobDone?jobName={metadata.Name}")
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

            await _kubernetes.CreateNamespacedJobAsync(job, @namespace);
        }
    }
}