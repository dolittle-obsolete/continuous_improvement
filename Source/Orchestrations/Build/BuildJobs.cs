
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
using Dolittle.Collections;

namespace Orchestrations.Build
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that is capable of dealing with compilation and packaging
    /// </summary>
    public class BuildJobs : IPerformer<Context>
    {
        readonly ILogger _logger;
        readonly Kubernetes _kubernetes;

        /// <summary>
        /// Initializes a new instance of <see cref="BuildJobs"/>
        /// </summary>
        /// <param name="kubernetes"><see cref="Kubernetes"/> client</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public BuildJobs(
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
        public Task Perform(Context context)
        {
            context.LogInformation("Building jobs");
            context.Project.Builds.ForEach(async _ => await StartJobFor(context, _));
            return Task.CompletedTask;
        }


        async Task StartJobFor(Context context, Read.Configuration.Build build)
        {
            var @namespace = "dolittle";

            var metadata = new V1ObjectMeta
            {
                Name = Guid.NewGuid().ToString() //,
                //Labels = { { "type", "build" } }
            };

            context.LogInformation($"---");
            context.LogInformation($"Type : {build.Type}");
            context.LogInformation($"BasePath : {build.BasePath}");
            context.LogInformation($"Package : {build.Package}");
            context.LogInformation($"Publish : {build.Publish}");
            context.LogInformation($"Folder with project to publish : {build.FolderWithProjectToPublish}");
            context.LogInformation($"---");


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
                                    Image = $"dolittlebuild/{build.Type}",
                                    ImagePullPolicy = "Always",
                                    Env = new [] {
                                        new V1EnvVar("REPOSITORY",context.Project.Repository.ToString()),
                                        new V1EnvVar("COMMIT",context.SourceControl.Commit),
                                        new V1EnvVar("PULL_REQUEST", context.IsPullRequest.ToString()),
                                        new V1EnvVar("VERSION", context.Version),
                                        new V1EnvVar("BASE_PATH", build.BasePath),
                                        new V1EnvVar("PACKAGE", build.Package.ToString()),
                                        new V1EnvVar("PUBLISH", build.Publish.ToString()),
                                        new V1EnvVar("FOLDER_WITH_PROJECT_TO_PUBLISH", build.FolderWithProjectToPublish),
                                        new V1EnvVar("CALLBACK", $"http://continuousimprovement/buildJobDone?buildJobName={metadata.Name}")
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