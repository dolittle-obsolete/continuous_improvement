/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;

namespace Policies.Improvements
{
    /// <summary>
    /// Defines a factory for building an <see cref="ImprovementContext" />
    /// </summary>
    public interface IImprovementContextFactory
    {
        /// <summary>
        /// Builds an improvement context
        /// </summary>
        /// <param name="improvable">The improvable being improved</param>
        /// <param name="version">The version associated with the improvement</param>
        /// <returns></returns>
        ImprovementContext GetFor(ImprovableId improvable, Version version);
    }
}