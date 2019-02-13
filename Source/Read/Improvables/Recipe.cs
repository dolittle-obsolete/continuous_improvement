/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;
using Concepts.Improvables;

namespace Read.Improvables
{

    public class Recipe
    {
        public RecipeType Type { get; set; }
        public bool Package { get; set; }
        public bool Publish { get; set; }
        public string BasePath { get; set; }
        public IEnumerable<DeploymentId> Deployment { get; set; }
        public IEnumerable<NotificationChannelId> NotificationChannels { get; set; }
    }
}
