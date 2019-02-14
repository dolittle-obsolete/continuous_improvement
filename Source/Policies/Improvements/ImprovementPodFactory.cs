/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Concepts.Improvements;
using Dolittle.Collections;
using k8s;
using k8s.Models;

namespace Policies.Improvements
{

    /// <summary>
    /// Represents an implementation of <see cref="IImprovementPodFactory"/>
    /// </summary>
    public class ImprovementPodFactory : IImprovementPodFactory
    {


        /// <inheritdoc/>
        public V1Pod BuildFrom(ImprovementContext context, IRecipe recipe)
        {
            var pod = new V1Pod {
                Metadata = new V1ObjectMeta {
                    Name = context.Improvement.Id.ToString(),
                    NamespaceProperty = "dolittle-builds",
                    Labels = {
                        {PodLabels.Name, context.Improvable.Name},
                        {PodLabels.RecipeType, recipe.GetType().Name},
                        {PodLabels.Version, context.Version},
                        {PodLabels.Tenant, context.Tenant.ToString()},
                        {PodLabels.Improvement, context.Improvement.Id.ToString()},
                        {PodLabels.Improvable, context.Improvement.Improvable.ToString()},
                    },
                },

                Spec = new V1PodSpec {
                    RestartPolicy = "Never",

                    Containers = new [] {
                        new V1Container {
                            Name = "done",
                            Image = "alpine:3.9",
                            Command = new [] { "/bin/true "},
                        },
                    },

                    Volumes = new [] {
                        new V1Volume {
                            Name = "azure",
                            AzureFile = new V1AzureFileVolumeSource {
                                ShareName = "continuousimprovement",
                                SecretName = "azure-storage-secret",
                                ReadOnlyProperty = false,
                            },
                        },
                        new V1Volume {
                            Name = "workdir",
                            EmptyDir = new V1EmptyDirVolumeSource { },
                        },
                    },
                },
            };

            var containers = pod.Spec.Containers = new List<V1Container>();

            // Copy the loghandler binary to the workdir before anything else
            containers.Add(new V1Container {
                Name = "copy-loghandler-to-workdir",
                Image = "alpine:3.9",
                Command = new [] { "/bin/cp", "/azure-binaries/loghandler", "/workdir-binaries/loghandler" },
                VolumeMounts = new [] {
                    new V1VolumeMount {
                        Name = "azure",
                        SubPath = "binaries",
                        MountPath = "/azure-binaries/",
                    },
                    new V1VolumeMount {
                        Name = "workdir",
                        SubPath = "binaries",
                        MountPath = "/workdir-binaries/",
                    },
                },
            });

            // Prepare the actual build steps
            StepNumber stepNumber = 0;
            recipe.GetStepsFor(context).ForEach(step => {
                StepNumber subStepNumber = 0;
                step.GetContainersFor(stepNumber, context).ForEach(subStep => {
                    containers.Add(subStep);

                    // Prepend the name of the container with step-N-M- to identify the step later
                    subStep.Name = $"step-{stepNumber}-{subStepNumber}-{subStep.Name}";

                    // Use the log handler to run the actual command
                    ThrowIfNoCommandIsSetForStepContainer(subStep);
                    subStep.Command.Insert(0, "/dolittle/loghandler");

                    if (subStep.Env == null) subStep.Env = new List<V1EnvVar>();
                    subStep.Env.Add(new V1EnvVar { Name = "DOLITTLE_BUILD_LOG_RAW_PATH", Value = $"/steps/{stepNumber}.log" });
                    subStep.Env.Add(new V1EnvVar { Name = "DOLITTLE_BUILD_LOG_PARSED_PATH", Value = $"/steps/{stepNumber}.json" });
                    subStep.Env.Add(new V1EnvVar { Name = "DOLITTLE_BUILD_LOG_PARSER", Value = step.GetLogParserNameFor(stepNumber, context) });

                    if (subStep.VolumeMounts == null) subStep.VolumeMounts = new List<V1VolumeMount>();
                    subStep.VolumeMounts.Add(new V1VolumeMount {
                        Name = "azure",
                        SubPath = context.GetImprovementSubPath("steps"),
                        MountPath = "/steps/",
                    });
                    subStep.VolumeMounts.Add(new V1VolumeMount {
                        Name = "workdir",
                        SubPath = "binaries",
                        MountPath = "/dolittle/",
                    });

                    subStepNumber++;
                });
                stepNumber++;
            });

            return pod;
        }

        void ThrowIfNoCommandIsSetForStepContainer(V1Container subStep)
        {
            if (subStep.Command == null || subStep.Command.Count < 1)
            {
                throw new StepContainerMustHaveCommand();
            }
        }
    }
}