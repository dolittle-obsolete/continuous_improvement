/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Concepts;
using Concepts.Improvements;
using Dolittle.Tenancy;
using Read.Improvables;
using Read.Improvements;

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
        /// <param name="improvement">The <see cref="Improvement">improvement</see></param>
        /// <param name="tenant">The <see cref="Tenant">tenant</see></param>
        /// <param name="improvableConfiguration">The <see cref="Read.Improvables.Improvable">Improvable</see></param>
        public ImprovementContext(
            TenantId tenant,
            Improvement improvement,
            Improvable improvable)
        {
            Improvement = improvement;
            Tenant = tenant;
            Improvable = improvable;
        }

        /// <summary>
        /// Gets the <see cref="Improvement"/> this context is for
        /// </summary>
        public Improvement Improvement { get; }

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
        /// Gets the <see cref="Read.Improvables.Improvable">configuraion</see> for the improvable
        /// </summary>
        public Improvable Improvable { get; }

        /// <summary>
        /// Gets the path to the files for the <see cref="Improvable"/>
        /// </summary>
        public string ImprovablePath => Path.Combine("/improvables/",Tenant.ToString(),Improvable.Id.ToString());

        /// <summary>
        /// Gets a sub-path to the files for the <see cref="Improvable"/>
        /// </summary>
        public string GetImprovableSubPath(string subPath) => Path.Combine(ImprovablePath, subPath);

        /// <summary>
        /// Gets the path to the files for the <see cref="Improvement"/>
        /// </summary>
        public string ImprovementPath => GetImprovableSubPath(Version);

        /// <summary>
        /// Gets a sub-path to the files for the <see cref="Improvement"/>
        /// </summary>
        public string GetImprovementSubPath(string subPath) => Path.Combine(ImprovementPath, subPath);
    }
}