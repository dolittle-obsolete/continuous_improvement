/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using k8s.Models;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a system that can create K8s Pods from improvement <see cref="IRecipe"/>
    /// </summary>
    public interface IImprovementPodFactory
    {
        /// <summary>
        /// Build a <see cref="V1Pod"/> from <see cref="IRecipe"/> based on <see cref="ImprovementContext"/>
        /// </summary>
        /// <param name="context"><see cref="ImprovementContext"/> to build for</param>
        /// <param name="recipe"><see cref="IRecipe"/> to build from</param>
        /// <returns><see cref="V1Pod"/> ready to run</returns>
        V1Pod BuildFrom(ImprovementContext context, IRecipe recipe);
    }
}