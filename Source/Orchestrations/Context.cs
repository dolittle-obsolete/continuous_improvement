/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Tenancy;
using Infrastructure.Orchestrations;
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents the continuous improvement context 
    /// </summary>
    public class Context : BaseContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> for the context</param>
        /// <param name="project"><see cref="Project"/> configuration</param>
        /// <param name="sourceControlContext"><see cref="SourceControlContext"/> to build</param>
        /// <param name="basePath">Base path for the build</param>
        /// <param name="buildNumber">Build number for the context</param>
        public Context(
            TenantId tenantId,
            Project project,
            SourceControlContext sourceControlContext,
            string basePath,
            int buildNumber) : base(basePath)
        {
            Tenant = tenantId;
            Project = project;
            BuildNumber = buildNumber;
            SourceControl = sourceControlContext;
            Version = $"1.0.0-{BuildNumber}";
            Volumes = new VolumePaths(this);

            if( !Directory.Exists(OutputPath)) Directory.CreateDirectory(OutputPath);
        }

        /// <summary>
        /// Gets the <see cref="TenantId"/> for the context
        /// </summary>
        public TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="Project"/> configuration object
        /// </summary>
        public Project Project { get; }

        /// <summary>
        /// Gets the <see cref="SourceControlContext"/>
        /// </summary>
        public SourceControlContext SourceControl { get; }

        /// <summary>
        /// Gets the build number for the context
        /// </summary>
        public int BuildNumber { get; }

        /// <summary>
        /// Gets whether or not the context represents a pull request 
        /// </summary>
        public bool IsPullRequest { get; }

        /// <summary>
        /// Gets the paths for volumes
        /// </summary>
        public VolumePaths Volumes { get; }

        /// <summary>
        /// Gets the path to the source
        /// </summary>
        public string SourcePath 
        {
            get
            {
                return SourceControl.IsPullRequest?
                            Path.Combine(BasePath,Version,"source"):
                            Path.Combine(BasePath,"source");
            }
        }

        /// <inheritdoc/>
        public override string OutputPath { get { return Path.Combine(BasePath, Version, "output"); } }

        /// <summary>
        /// Gets or sets the  version for the context
        /// </summary>
        public string Version { get; set; } 
    }
}