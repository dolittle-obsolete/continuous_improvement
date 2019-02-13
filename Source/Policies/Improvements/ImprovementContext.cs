/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Concepts;
using Concepts.Improvements;
using Dolittle.Tenancy;
using Read.Improvables;

namespace Policies.Improvements
{
    /// <summary>
    /// Represents the context of an improvement
    /// </summary>
    public class ImprovementContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ImprovementContext"/>
        /// </summary>
        /// <param name="improvement">The <see cref="ImprovementId">improvement</see></param>
        /// <param name="tenant">The <see cref="Tenant">tenant</see></param>
        /// <param name="version">The <see cref="VersionString">version</see></param>
        /// <param name="pullRequest"></param>
        /// <param name="improvableConfiguration">The <see cref="ImprovableConfiguration">configuration</see> of the improvable</param>
        public ImprovementContext(
            ImprovementId improvement,
            TenantId tenant,
            VersionString version,
            bool pullRequest,
            ImprovableConfiguration configuration)
        {
            Improvement = improvement;
            Tenant = tenant;
            Version = version;
            PullRequest = pullRequest;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the <see cref="Improvement"/> this context is for
        /// </summary>
        public ImprovementId Improvement { get; }

        /// <summary>
        /// Gets the <see cref="Tenant">tenant</see> in which the improvement is for
        /// </summary>
        public TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="VersionString">version</see> for the improvement
        /// </summary>
        public VersionString Version { get; }

        /// <summary>
        /// Gets whether or not it was a pull request that caused the improvement to happen
        /// </summary>
        public bool PullRequest { get; }

        /// <summary>
        /// Gets the <see cref="ImprovableConfiguration">configuraion</see> for the improvable
        /// </summary>
        public ImprovableConfiguration Configuration { get; }
    }
}