/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.SourceControl.GitHub;
using System.Linq;
using Concepts.SourceControl;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Represents an installation of an application on GitHub
    /// </summary>
    public class Installation
    {
        /// <summary>
        /// Instantiates an instance of <see cref="Installation" />
        /// </summary>
        /// <param name="id">The id of the installation</param>
        /// <param name="targetType">The type of the GitHub account</param>
        /// <param name="targetAccount">The login details for the GitHub account</param>
        /// <param name="repositories">A collection of repositories associated with the installation</param>
        public Installation(InstallationId id, AccountType targetType, AccountLogin targetAccount, IEnumerable<RepositoryFullName> repositories)
        {
            Id = id;
            TargetType = targetType;
            TargetAccount = targetAccount;
            Repositories = repositories;
        }
        /// <summary>
        /// The id of the installation
        /// </summary>
        public InstallationId Id { get; }
        /// <summary>
        /// The type of the GitHub account
        /// </summary>
        public AccountType TargetType { get; }
        /// <summary>
        /// The login details for the GitHub account
        /// </summary>
        public AccountLogin TargetAccount { get; }
        /// <summary>
        /// A collection of repositories associated with the installation
        /// </summary>
        public IEnumerable<RepositoryFullName> Repositories { get; }
    }
}
