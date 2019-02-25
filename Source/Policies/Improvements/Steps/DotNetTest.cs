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
    public class DotNetTest : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("f4dc887e-13c9-4340-8923-d9c360250b37");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            return new [] {
                new V1Container {
                    Name = "dotnet-test",
                    Image = "microsoft/dotnet:2.2-sdk-bionic",
                    Command = new [] {
                        "/bin/bash", "-c",
                        "for TEST_PROJECT in ./Specifications/**/*.csproj; do dotnet test $TEST_PROJECT --no-build --configuration=Release --logger=\"trx;LogFileName=$TEST_PROJECT.trx\" --results-directory=/tests/ ; done"
                    },
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
                        new V1VolumeMount {
                            Name = "azure",
                            SubPath = context.GetImprovementSubPath("tests"),
                            MountPath = "/tests/",
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