/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Tenancy;
using Read.Configuration;

namespace Orchestrations
{

    /// <summary>
    /// Represents the continuous improvement context 
    /// </summary>
    public partial class Context
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>
        /// </summary>
        /// <param name="project"><see cref="Project"/> configuration</param>
        /// <param name="sourceControlContext"><see cref="SourceControlContext"/> to build</param>
        public Context(
            TenantId tenantId,
            Project project,
            SourceControlContext sourceControlContext,
            string basePath,
            int buildNumber)
        {
            Tenant = tenantId;
            Project = project;
            BasePath = basePath;
            BuildNumber = buildNumber;
            Version = $"1.0.0-{BuildNumber}";
            Volumes = new VolumePaths(this);
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
        /// Gets the path to the folder where the source is located
        /// </summary>
        public string BasePath { get; }

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

        /// <summary>
        /// Gets or sets the  version for the context
        /// </summary>
        public string Version { get; set; } 

        /// <summary>
        /// Append information to log file
        /// </summary>
        /// <param name="message">Message to append</param>
        public void LogInformation(string message)
        {
            var outputFolder = Volumes.OutputPath;
            var logFile = Path.Combine(outputFolder,"logs.txt");
            if( !Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            File.AppendAllText(logFile,$"{message}\n");
            Console.WriteLine(message);
        }
    }
}