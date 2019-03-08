/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Collections;
using Dolittle.IO.Tenants;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Serialization.Json;

namespace Read.Improvables
{

    /// <summary>
    /// Represents all the <see cref="Project">projects</see>
    /// </summary>
    public class AllImprovables : IQueryFor<ImprovableForListing>
    {
        const string _improvablesFile = "improvables.json";
        readonly IFiles _fileSystem;

        /// <summary>
        /// Initializes a new instance of <see cref="AllImprovables"/>
        /// </summary>
        public AllImprovables(IFiles fileSystem, ISerializer serializer)
        {
            _fileSystem = fileSystem;
            if (_fileSystem.Exists(_improvablesFile))
            {
                var json = _fileSystem.ReadAllText(_improvablesFile);
                Query = serializer.FromJson<IEnumerable<ImprovableForListing>>(json).AsQueryable();
            }
            else
            {
                Query = new ImprovableForListing[0].AsQueryable();
            }
        }

        /// <summary>
        /// The query that will execute
        /// </summary>
        public IQueryable<ImprovableForListing> Query { get; }
    }
}