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
    public class DotNetBuild : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("602f452f-d7de-4f77-a16b-ee88c85a2921");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            return new [] {
                new V1Container {
                    Name = "dotnet-restore",
                    Image = "microsoft/dotnet:2.2-sdk-bionic",
                    Command = new [] { "/usr/bin/dotnet", "restore", "--force", "--packages=/nuget/" },
                    WorkingDir = "/source/",
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
                    },
                },
                new V1Container {
                    Name = "dotnet-build",
                    Image = "microsoft/dotnet:2.2-sdk-bionic",
                    Command = new [] { "/usr/bin/dotnet", "build", "--no-restore", "--packages=/nuget/", "--configuration=Release" },
                    WorkingDir = "/source/",
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