/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a recipe for an improvement
    /// </summary>
    public interface IRecipe
    {
        /// <summary>
        /// Gets the <see cref="IStep">steps</see> for the <see cref="IRecipe"/>
        /// </summary>
        IEnumerable<IStep>  GetStepsFor(ImprovementContext context);
    }
}