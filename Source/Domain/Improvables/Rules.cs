/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;

namespace Domain.Improvables
{
    /// <summary>
    /// Indicates whether or not there is an <see cref="Improvable" /> with the specified <see cref="ImprovableName" />
    /// </summary>
    /// <param name="name">The name of the improvable</param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool ImprovableNameExists(ImprovableName name);
}