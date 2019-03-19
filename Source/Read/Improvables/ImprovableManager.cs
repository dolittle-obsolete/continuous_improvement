/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Improvables;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Serialization.Json;

namespace Read.Improvables
{
    /// <inheritdoc />
    public class ImprovableManager : IImprovableManager
    {
        const string _improvablesFile = "improvables.json";
        const string _improvableFile = "improvable.json";

        private readonly ISerializer _serializer;
        private readonly IRecipeManager _recipeManager;
        private readonly IFiles _fileSystem;

        /// <summary>
        /// Instantiates an instance of <see cref="ImprovableManager" />
        /// </summary>
        /// <param name="fileSystem">A file system wrapper</param>
        /// <param name="serializer">A serializer</param>
        /// <param name="recipeManager">A recipe manager</param>
        public ImprovableManager(IFiles fileSystem, ISerializer serializer, IRecipeManager recipeManager)
        {
            _serializer = serializer;
            _recipeManager = recipeManager;
            _fileSystem = fileSystem;
        }
        
        /// <inheritdoc />
        public IEnumerable<ImprovableForListing> GetAllForListing(ImprovableId improvableId)
        {
            if (_fileSystem.Exists(_improvablesFile))
            {
                var json = _fileSystem.ReadAllText(_improvablesFile);
                return _serializer.FromJson<IEnumerable<ImprovableForListing>>(json);
            }
            else
            {
                return new ImprovableForListing[0];
            }
        }

        /// <inheritdoc />
        public Improvable GetById(ImprovableId improvableId)
        {
            var improvableFile = Path.Combine(improvableId.ToString(), _improvableFile);
            var json = _fileSystem.ReadAllText(improvableFile);
            var improvable = _serializer.FromJson<Improvable>(json);
            return improvable;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Save(Improvable improvable)
        {
            var improvableFile = Path.Combine(improvable.Id.ToString(), _improvableFile);
            var json = _serializer.ToJson(improvable);
            _fileSystem.WriteAllText(improvableFile, json);
        }
    }
}