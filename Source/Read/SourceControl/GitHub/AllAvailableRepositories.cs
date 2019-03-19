/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace Read.SourceControl.GitHub
{
    /// <summary>
    /// Gets all available repositories
    /// </summary>
    public class AllAvailableRepositories : IQueryFor<RepositoriesList>
    {
        readonly IReadModelRepositoryFor<RepositoriesList> _repositoryForRepositoriesList;
        /// <summary>
        /// Instantiates an instance of <see cref="AllAvailableRepositories" />
        /// </summary>
        /// <param name="repositoryForRepositoriesList"></param>
        public AllAvailableRepositories(IReadModelRepositoryFor<RepositoriesList> repositoryForRepositoriesList)
        {
            _repositoryForRepositoriesList = repositoryForRepositoriesList;
        }

        /// <inheritdoc />
        public IQueryable<RepositoriesList> Query => _repositoryForRepositoriesList.Query;
    }
}
