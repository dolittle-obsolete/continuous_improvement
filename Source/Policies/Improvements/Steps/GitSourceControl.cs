/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Concepts.Improvements;
using Dolittle.Events;
using k8s.Models;

namespace Policies.Improvements.Steps
{
    /// <summary>
    /// Represents the step type for dealing with Git source control
    /// </summary>
    public class GitSourceControl : IStep
    {
        /// <inheritdoc/>
        public StepType Type => new Guid("e8529c7a-b6a3-4447-a76b-7aaf32bfdff4");

        /// <inheritdoc/>
        public IEnumerable<V1Container> GetContainersFor(StepNumber number, ImprovementContext context)
        {
            var containers = new List<V1Container>();

            if (!context.PullRequest)
            {
                containers.Add(new V1Container {
                    Name = "git-update-repository",
                    Image = "dolittlebuild/sourcecontrol-git-update:1.0.0",
                    Command = new [] { "/bin/sh", "/usr/bin/update_repository.sh", "/source/", "" }, // FIXME: Get the RepositoryURL
                    VolumeMounts = new [] {
                        new V1VolumeMount {
                            Name = "azure",
                            SubPath = context.GetImprovableSubPath("source"),
                            MountPath = "/source/",
                        },
                    },
                });
            }

            containers.Add(new V1Container {
                Name = "copy-source-from-repository",
                Image = "alpine:3.9",
                Command = new [] { "/bin/cp", "-R", "/repository/.", "/source/" },
                VolumeMounts = new [] {
                    new V1VolumeMount {
                        Name = "azure",
                        SubPath = context.GetImprovableSubPath("source"),
                        MountPath = "/repository/",
                    },
                    new V1VolumeMount {
                        Name = "workdir",
                        SubPath = "source",
                        MountPath = "/source/",
                    },
                },
            });

            if (context.PullRequest)
            {
                // FIXME: We need to checkout the merged sha as well!
                throw new System.NotImplementedException();
            }

            return containers;
        }

        /// <inheritdoc/>
        public LogParserName GetLogParserNameFor(StepNumber number, ImprovementContext context) => "git";

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetFailedEventsFor(ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEvent> GetSucceededEventsFor(ImprovementContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}