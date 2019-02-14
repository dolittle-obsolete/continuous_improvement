/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using Read.Configuration;

namespace Read.Improvables
{
    public class RecipeManager : IRecipeManager
    {
        readonly IDeploymentManager _deploymentManager;
        readonly INotificationChannelManager _notificationChannelManager;

        public RecipeManager(IDeploymentManager deploymentManager, INotificationChannelManager notificationChannelManager)
        {
            _deploymentManager = deploymentManager;
            _notificationChannelManager = notificationChannelManager;
        }
        
        public ExpandedRecipe Expand(Recipe recipe)
        {
            var expandedRecipe = new ExpandedRecipe
            {
                Type = recipe.Type,
                Package = recipe.Package,
                Publish = recipe.Publish,
                BasePath = recipe.BasePath,
                Deployments = recipe.Deployments.Select(_ => _deploymentManager.GetById(_)).ToArray(),
                NotificationChannels = recipe.NotificationChannels.Select(_ => _notificationChannelManager.GetById(_)).ToArray()
            };

            return expandedRecipe;
        }
    }
}