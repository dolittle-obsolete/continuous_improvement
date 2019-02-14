/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Improvables;
using Dolittle.Serialization.Json;

namespace Read.Improvables
{
    public class ImprovableManager : IImprovableManager
    {

        string _tenantPath;
        private readonly ISerializer _serializer;
        private readonly IRecipeManager _recipeManager;

        public ImprovableManager(ISerializer serializer, IRecipeManager recipeManager)
        {
            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty;
            _tenantPath = Path.Combine(basePath, "508c1745-5f2a-4b4c-b7a5-2fbb1484346d");
            _serializer = serializer;
            _recipeManager = recipeManager;
        }
        
        public IEnumerable<ImprovableForListing> GetAllForListing(ImprovableId improvableId)
        {
            var improvablesFile = Path.Combine(_tenantPath, "improvables.json");
            if (File.Exists(improvablesFile))
            {
                var json = File.ReadAllText(improvablesFile);
                return _serializer.FromJson<IEnumerable<ImprovableForListing>>(json);
            }
            else
            {
                return new ImprovableForListing[0];
            }
        }

        public Improvable GetById(ImprovableId improvableId)
        {
            var improvableFile = Path.Combine(_tenantPath, improvableId.ToString(), "improvable.json");
            var json = File.ReadAllText(improvableFile);
            var improvable = _serializer.FromJson<Improvable>(json);
            return improvable;
        }

        public ExpandedImprovable GetExpandedById(ImprovableId improvableId)
        {
            var improvable = GetById(improvableId);
            var expandedImprovable = new ExpandedImprovable
            {
                Id = improvable.Id,
                Name = improvable.Name,
                SourceControl = improvable.SourceControl,
                Status = improvable.Status,
                Recipes = improvable.Recipes.Select(_ => _recipeManager.Expand(_)).ToArray()
            };
            return expandedImprovable;
        }


        public void Save(Improvable improvable)
        {
            var improvableFile = Path.Combine(_tenantPath, improvable.Id.ToString(), "improvable.json");
            var json = _serializer.ToJson(improvable);
            File.WriteAllText(improvableFile, json);
        }
    }
}