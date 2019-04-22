/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Domain.Improvables;
using Dolittle.Rules;
using Read.Improvables;
using Dolittle.ReadModels;
using Domain;

namespace Rules.Improvables
{
    /// <summary>
    /// Implementation of Domain.Improvables.ImprovableExists
    /// </summary>
    public class GetImprovableExists : IRuleImplementationFor<ImprovableExists>
    {
        private readonly IImprovableManager _improvableManager;

        /// <summary>
        /// Instantiates the implemenation
        /// </summary>
        /// <param name="improvableManager">Improvable Manager for fetching <see cref="Read.Improvables.Improvable">improvables</see></param>
        public GetImprovableExists(IImprovableManager improvableManager)
        {
            _improvableManager = improvableManager;
        } 

        /// <summary>
        /// Gets the implementation
        /// </summary>
        /// <returns>An implementaion of the <see cref="ImprovableExists" /> delegate</returns>
        public ImprovableExists Rule => (id) => _improvableManager.GetById(id) != null;
    }
}