/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Runtime.Tenancy;
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents the continuous improvement context 
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>
        /// </summary>
        /// <param name="project"><see cref="Project"/> configuration</param>
        /// <param name="commit"><see cref="string">Commit</see> to build</param>
        /// <remarks>
        /// Commit is typically the SHA in Git for instance - currently Git is the only supported
        /// source control provider
        /// </remarks>
        public Context(
            TenantId tenantId,
            Project project,
            string basePath,
            string commit,
            int buildNumber,
            bool isPullRequest)
        {
            Tenant = tenantId;
            Project = project;
            BasePath = basePath;
            Commit = commit;
            BuildNumber = buildNumber;
            IsPullRequest = isPullRequest;
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
        /// Gets the path to the folder where the source is located
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Gets the commit that triggered the build
        /// </summary>
        public string Commit { get; }

        /// <summary>
        /// Gets the build number for the context
        /// </summary>
        public int BuildNumber { get; }

        /// <summary>
        /// Gets whether or not the context represents a pull request 
        /// </summary>
        public bool IsPullRequest { get; }

        /// <summary>
        /// Gets or sets the  version for the context
        /// </summary>
        public string Version { get; set;  } = "1.0.0";

        /// <summary>
        /// Gets the root folder of the project
        /// </summary>
        public string ProjectRoot => Path.Combine(Tenant.ToString(), Project.Id.ToString());

        /// <summary>
        /// Gets the root folder of the version being built
        /// </summary>
        public string VersionRoot => Path.Combine(ProjectRoot, Version);

        /// <summary>
        /// Gets the path to the packages - typically used for deployable packages
        /// </summary>
        public string PackagePath => Path.Combine(VersionRoot,"packages");

        /// <summary>
        /// Gets the path to the source
        /// </summary>
        public string SourcePath => Path.Combine(ProjectRoot,"source");

        /// <summary>
        /// Gets the path to the output - typically used for build logs
        /// </summary>
        public string OutputPath => Path.Combine(VersionRoot,"output");

        /// <summary>
        /// Gets the path to publishable artifacts
        /// </summary>
        public string PublishPath => Path.Combine(VersionRoot,"public");

        /// <summary>
        /// Gets the full path to the source 
        /// </summary>
        public string FullSourcePath => Path.Combine(BasePath, SourcePath);

        /// <summary>
        /// Gets the full path to the log file
        /// </summary>
        public string LogFile => Path.Combine(BasePath, OutputPath, "log.txt");

        /// <summary>
        /// Gets the path to test results
        /// </summary>
        /// <value></value>
        public string TestResultsPath { get; }

        /// <summary>
        /// Append information to log file
        /// </summary>
        /// <param name="message">Message to append</param>
        public void LogInformation(string message)
        {
            var outputFolder = Path.GetDirectoryName(LogFile);
            if( !Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            File.AppendAllText(LogFile,$"{message}\n");
            Console.WriteLine(message);
        }
    }
}