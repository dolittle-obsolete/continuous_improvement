/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;

namespace Domain
{
    /// <summary>
    /// Indicates whether or not there is an <see cref="ImprovableId" /> with the specified Id
    /// </summary>
    /// <param name="id">The Id of the improvable</param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool ImprovableExists(ImprovableId id);
}