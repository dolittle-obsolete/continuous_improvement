/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;

namespace Read.Improvables
{
    /// <summary>
    /// Defines an improvable manager
    /// </summary>
    public interface IImprovableManager 
    {
        /// <summary>
        /// Gets all the improvablefor listings for an improvable
        /// </summary>
        /// <param name="improvableId">The improvable id to get the improvablefors</param>
        IEnumerable<ImprovableForListing> GetAllForListing(ImprovableId improvableId);
        /// <summary>
        /// Gets the <see cref="Improvable" /> for the specific id
        /// </summary>
        /// <param name="improvableId">The id of the improvable to get</param>
        /// <returns></returns>
        Improvable GetById(ImprovableId improvableId);
        /// <summary>
        /// Gets the <see cref="ExpandedImprovable" /> for the specific id
        /// </summary>
        /// <param name="improvableId"></param>
        /// <returns></returns>
        ExpandedImprovable GetExpandedById(ImprovableId improvableId);
        /// <summary>
        /// Saves an <see cref="Improvable" />
        /// </summary>
        /// <param name="improvable"></param>
        void Save(Improvable improvable);
    }
}