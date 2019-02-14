/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;

namespace Read.Improvables
{

    public interface IImprovableManager 
    {
        IEnumerable<ImprovableForListing> GetAllForListing(ImprovableId improvableId);
        Improvable GetById(ImprovableId improvableId);
        ExpandedImprovable GetExpandedById(ImprovableId improvableId);
        void Save(Improvable improvable);
    }
}