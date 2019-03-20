/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;
using Concepts.Improvables;

namespace Read.Improvables
{
    /// <summary>
    /// A recipe for steps in an improvement
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// The recipe type
        /// </summary>
        public RecipeType Type { get; set; }
        /// <summary>
        /// A flag indicating if the recipe will package
        /// </summary>
        public bool Package { get; set; }
        /// <summary>
        /// A flag indicating if the recipe will publish
        /// </summary>
        public bool Publish { get; set; }
        /// <summary>
        /// The base path of the reciåe
        /// </summary>
        public string BasePath { get; set; }
        /// <summary>
        /// A list of <see cref="DeploymentId">deployments</see> associated with this recipe
        /// </summary>
        public IEnumerable<DeploymentId> Deployments { get; set; }
        /// <summary>
        /// A list of <see cref="NotificationChannelId">notification channels</see> associated with this recipe
        /// </summary>        
        public IEnumerable<NotificationChannelId> NotificationChannels { get; set; }
    }
}
