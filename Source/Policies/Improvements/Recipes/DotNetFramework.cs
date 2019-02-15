/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Policies.Improvements.Steps;

namespace Policies.Improvements.Recipes
{
    /// <summary>
    /// Represents the recipe for .NET Framework based frameworks
    /// </summary>
    public class DotNetFramework : IRecipe
    {
        /// <inheritdoc/>
        public IEnumerable<IStep> GetStepsFor(ImprovementContext context)
        {
            return new IStep[]
            {
                new GitSourceControl(),
                new DotNetBuild(),
                new DotNetTest(),
                new NuGetRelease(),
            };
        }
    }
}