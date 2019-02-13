/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Represents an implementation of <see cref="IImprovementPodFactory"/>
    /// </summary>
    public class ImprovementPodFactory : IImprovementPodFactory
    {
        /// <inheritdoc/>
        public V1PodTemplateSpec BuildFrom(ImprovementContext context, IRecipe recipe)
        {
            var podTemplate = new V1PodTemplateSpec();
            podTemplate.Metadata = new V1ObjectMeta {
                Name = $"{context.Improvable.Name} - {context.Version}",
                NamespaceProperty = "dolittle-build",
                Labels = {
                    {"Name", context.Improvable.Name},
                    {"Tenant", context.Tenant.ToString()},
                    {"Improvement", context.Improvement.Id.ToString()},
                    {"Improvable", context.Improvement.Improvable.ToString()},
                    {"Version", context.Version}
                }
            };
            
            var pod =  new V1PodSpec();
            podTemplate.Spec = pod;

            var containers = new List<V1Container>();
            var steps = recipe.GetStepsFor(context).ToArray();
            for( var stepIndex=0; stepIndex<steps.Length; stepIndex++ ) 
            {
                var step = steps[stepIndex];
                var stepContainers = step.GetContainersFor(stepIndex, context);
                var stepNumber = (float)stepIndex;
                stepContainers.ForEach(_ => {
                    _.Name = stepNumber.ToString();
                    stepNumber += 0.1f;
                });
                containers.AddRange(stepContainers);
            }

            pod.Containers = containers;

            pod.Volumes = new[] {
                new V1Volume {
                    Name = "azure",
                    AzureFile = new V1AzureFileVolumeSource {
                        SecretName = "azure-storage-secret",
                        ShareName = "continuousimprovement",
                        ReadOnlyProperty = false
                    }
                }
            };
            pod.RestartPolicy = "Never";

            return podTemplate;
        }
    }
}