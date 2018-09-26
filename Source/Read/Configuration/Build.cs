/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Configuration
{
    /// <summary>
    /// Represents a build
    /// </summary>
    public class Build 
    {
         /// <summary>
        /// Gets or sets the type of project
        /// </summary>
       public string Type { get; set; } = string.Empty;

       /// <summary>
       /// Gets or sets whether or not to package
       /// </summary>
        public bool Package { get; set; } = false;

       /// <summary>
       /// Gets or sets whether or not to publish
       /// </summary>
        public bool Publish { get; set; } = false;

        /// <summary>
        /// Gets or sets the base path to build from
        /// </summary>
        public string BasePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the relative folder with project to publish - relative to base path within the repository
        /// </summary>
        public string FolderWithProjectToPublish { get; set; } = string.Empty;
    }
}