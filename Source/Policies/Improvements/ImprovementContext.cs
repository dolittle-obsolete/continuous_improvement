/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.Tenancy;

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
        /// <param name="tenant">The <see cref="Tenant">tenant</see></param>
        /// <param name="project">The <see cref="ProjectId">project</see></param>
        /// <param name="version">The <see cref="VersionString">version</see></param>
        public ImprovementContext(
            TenantId tenant,
            ProjectId project,
            VersionString version)
        {
            Tenant = tenant;
            Project = project;
            Version = version;
        }

        /// <summary>
        /// Gets the <see cref="Tenant">tenant</see> in which the improvement is for
        /// </summary>
        public TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="ProjectId">project</see> the improvement is for
        /// </summary>
        public ProjectId Project { get; }

        /// <summary>
        /// Gets the <see cref="VersionString">version</see> for the improvement
        /// </summary>
        public VersionString Version { get; }
    }
}