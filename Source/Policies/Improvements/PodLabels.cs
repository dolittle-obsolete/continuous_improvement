/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Policies.Improvements
{
    /// <summary>
    /// Represents labels used on K8s pods scheduled
    /// </summary>
    public class PodLabels
    {
        /// <summary>
        /// Type of recipe a step belongs to
        /// </summary>
        public const string RecipeType = "RecipeType";

        /// <summary>
        /// Version being improved towards
        /// </summary>
        public const string Version = "Version";

        /// <summary>
        /// Origin tenant
        /// </summary>
        public const string Tenant = "Tenant";

        /// <summary>
        /// Identifier for the improvement being improved
        /// </summary>
        public const string Improvement = "Improvement";

        /// <summary>
        /// Identifier of the improvable being improved
        /// </summary>
        public const string Improvable = "Improvable";
    }
}