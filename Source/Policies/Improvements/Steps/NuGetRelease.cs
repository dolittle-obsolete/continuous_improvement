/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.Improvements;
using Dolittle.Events;
using k8s.Models;

namespace Policies.Improvements.Steps
{
    /// <summary>
    /// Represents the step type for dealing with Git source control
    /// </summary>
    public class NuGetRelease : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("129101ae-058e-4a01-8ba4-6726bbc9b2f9");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            return new [] {
                new V1Container {
                    Name = "dotnet-package",
                    Image = "microsoft/dotnet:2.2-sdk-bionic",
                    WorkingDir = "/source/",
                    Command = new [] {
                        "/usr/bin/dotnet", "pack",
                        "--no-build",
                        "--packages=/nuget/",
                        "--configuration=Release",
                        "--output=/output/",
                        "--include-symbols",
                        "--include-source",
                        $"-p:PackageVersion={context.Version}"
                    },
                    VolumeMounts = new [] {
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "source",
                            MountPath = "/source/",
                        },
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "nuget",
                            MountPath = "/nuget/",
                        },
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "output",
                            MountPath = "/output/",
                        },
                    },
                },
                new V1Container {
                    Name = "move-symbols-nupkg",
                    Image = "alpine:3.9",
                    WorkingDir = "/output/",
                    Command = new [] { "/bin/sh", "-c", "for NUPKG in *.symbols.nupkg; do cp $NUPKG /publish/${NUPKG/.symbols/} ; mv $NUPKG /nuget/${NUPKG/.symbols/} ; done"},
                    VolumeMounts = new [] {
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "output",
                            MountPath = "/output/",
                        },
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "publish",
                            MountPath = "/publish/",
                        },
                        new V1VolumeMount {
                            Name = "azure",
                            SubPath = context.GetImprovementSubPath("nuget"),
                            MountPath = "/nuget/",
                        },
                    },
                },
                new V1Container {
                    Name = "nuget-push",
                    Image = "microsoft/dotnet:2.2-sdk-bionic",
                    WorkingDir = "/publish/",
                    Command = new [] {
                        "/usr/bin/dotnet", "nuget", "push", "./",
                        "--source=https://www.myget.org/F/dolittle/api/v2/package",
                        "--api-key=SECRET_HERE",
                    },
                    VolumeMounts = new [] {
                        new V1VolumeMount {
                            Name = "workdir",
                            SubPath = "publish",
                            MountPath = "/publish/",
                        },
                    },
                },
            };
        }

        /// <inheritdoc/>
        public LogParserName GetLogParserNameFor(StepNumber number, ImprovementContext context) => "dotnet";

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetFailedEventsFor(ImprovementContext context)
        {
            // TODO: Add events
            return Enumerable.Empty<IEvent>();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetSucceededEventsFor(ImprovementContext context)
        {
            // TODO: Add events
            return Enumerable.Empty<IEvent>();
        }
    }
}