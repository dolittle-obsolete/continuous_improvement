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
    /// Gets all installations
    /// </summary>
    public class AllInstallations : IQueryFor<InstallationsList>
    {
        readonly IReadModelRepositoryFor<InstallationsList> _repositoryForInstallationsList;
        /// <summary>
        /// Instantiates an instance of <see cref="AllInstallations" />
        /// </summary>
        /// <param name="repositoryForInstallationsList">a repsository for installations list</param>
        public AllInstallations(IReadModelRepositoryFor<InstallationsList> repositoryForInstallationsList)
        {
            _repositoryForInstallationsList = repositoryForInstallationsList;
        }

        /// <inheritdoc />
        public IQueryable<InstallationsList> Query => _repositoryForInstallationsList.Query;
    }
}
