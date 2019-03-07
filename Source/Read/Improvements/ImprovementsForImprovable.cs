/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Queries;

namespace Read.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class ImprovementsForImprovable : IQueryFor<Improvement>
    {
        private readonly IFiles _fileSystem;

        public ImprovementsForImprovable(IFiles fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// 
        /// </summary>
        public ImprovableId Improvable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Improvement> Query 
        { 
            get
            {
                var improvements = _fileSystem.GetDirectoriesIn(Improvable.Value.ToString());
                return improvements.Select(_ => 
                {
                    var segments = _.Split(Path.DirectorySeparatorChar);
                    return new Improvement { Version = segments[segments.Length-1] };
                }).AsQueryable();
            }
        }      
    }
}