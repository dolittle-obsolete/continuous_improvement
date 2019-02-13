/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;
using Read.Configuration;

namespace Read.Improvables
{
    public class ExpandedRecipe
    {
        public RecipeType Type { get; set; }
        public bool Package { get; set; }
        public bool Publish { get; set; }
        public string BasePath { get; set; }
        public IEnumerable<Deployment> Deployment { get; set; }
        public IEnumerable<NotificationChannel> NotificationChannels { get; set; }
    }
}
