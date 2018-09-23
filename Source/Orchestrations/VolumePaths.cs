/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Tenancy;
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Represents the paths for mounting volumes
    /// </summary>
    public class VolumePaths
    {
        readonly Context _context;

        /// <summary>
        /// Initializes a new instance of <see cref="VolumePaths"/>
        /// </summary>
        /// <param name="context"><see cref="Context"/> for the paths</param>
        public VolumePaths(Context context)
        {
           _context = context;
        }

        /// <summary>
        /// Gets the root folder of the project
        /// </summary>
        public string Root => Path.Combine(_context.Tenant.ToString(), _context.Project.Id.ToString());

        /// <summary>
        /// Gets the root folder of the version being built
        /// </summary>
        public string VersionRoot => Path.Combine(Root, _context.Version);

        /// <summary>
        /// Gets the path to the packages - typically used for deployable packages
        /// </summary>
        public string PackagePath => Path.Combine(VersionRoot,"packages");

        /// <summary>
        /// Gets the path to the source
        /// </summary>
        public string SourcePath => Path.Combine(Root,"source");

        /// <summary>
        /// Gets the path to the output - typically used for build logs
        /// </summary>
        public string OutputPath => Path.Combine(VersionRoot,"output");

        /// <summary>
        /// Gets the path to publishable artifacts
        /// </summary>
        public string PublishPath => Path.Combine(VersionRoot,"publish");

        /// <summary>
        /// Gets the path to test results
        /// </summary>
        public string TestResultsPath => Path.Combine(VersionRoot,"testresults");
    }
}