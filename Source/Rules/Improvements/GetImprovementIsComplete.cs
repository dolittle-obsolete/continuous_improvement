/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using Domain.Improvements;
using Dolittle.Rules;
using Dolittle.ReadModels;

namespace Rules.Improvements
{
    /// <summary>
    /// Implementation of Domain.Improvement.ImprovementIsComplete
    /// </summary>
    public class GetImprovementIsComplete : IRuleImplementationFor<ImprovementIsComplete>
    {
        private readonly IReadModelRepositoryFor<Read.Improvements.Improvement> _repo;

        /// <summary>
        /// Instantiates the implemenation
        /// </summary>
        /// <param name="repo"></param>
        public GetImprovementIsComplete(IReadModelRepositoryFor<Read.Improvements.Improvement> repo)
        {
            _repo = repo;
        } 

        /// <summary>
        /// Gets the implementation
        /// </summary>
        /// <returns></returns>
        public ImprovementIsComplete Rule => (id) => _repo.GetById(id)?.HasCompleted ?? false;
    }
}