/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;
using Read.Configuration;

namespace Read.Improvables
{
    /// <summary>
    /// Expands an <see cref="Recipe" /> with additional information
    /// </summary>
    public class ExpandedRecipe
    {
        /// <summary>
        /// The recipe type
        /// </summary>
        public RecipeType Type { get; set; }
        /// <summary>
        /// Flag indicating if the recipe will package
        /// </summary>
        public bool Package { get; set; }
        /// <summary>
        /// Flag indicating if the recipe will publish
        /// </summary>
        public bool Publish { get; set; }
        /// <summary>
        /// Base path of the recipe
        /// </summary>
        public string BasePath { get; set; }
        /// <summary>
        /// A list of <see cref="Deployment">deployments</see>
        /// </summary>
        public IEnumerable<Deployment> Deployments { get; set; }
        /// <summary>
        /// A list of <see cref="NotificationChannel">notification channels</see>
        /// </summary>
        public IEnumerable<NotificationChannel> NotificationChannels { get; set; }
    }
}
