/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.IO;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents a base context
    /// </summary>
    public class BaseContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BaseContext"/>
        /// </summary>
        /// <param name="basePath"></param>
        public BaseContext(string basePath)
        {
            BasePath = basePath;
        }

        /// <summary>
        /// Gets the base path
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Gets the output path
        /// </summary>
        public virtual string OutputPath { get { return Path.Combine(BasePath,"output"); } }
    }
}