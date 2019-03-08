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
using Dolittle.Collections;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Queries;
using Dolittle.Serialization.Json;

namespace Read.Improvements
{
    /// <summary>
    /// 
    /// </summary>
    public class StepResultsForStep : IQueryFor<StepResult>
    {
        readonly ISerializer _serializer;
        private readonly IFiles _fileSystem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        public StepResultsForStep(IFiles fileSystem, ISerializer serializer)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// 
        /// </summary>
        public ImprovableId Improvable {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Concepts.Version Version {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StepNumber Number {  get; set; }

        /// <inheritdoc/>
        public IQueryable<StepResult> Query
        {
            get
            {
                var versionPath = Path.Combine(Improvable.Value.ToString(), Version);

                var stepsPath = Path.Combine(versionPath, "steps");

                var stepFilePath = Path.Combine(stepsPath, $"{Number}.json");
                if (_fileSystem.Exists(stepFilePath))
                {
                    var content = _fileSystem.ReadAllText(stepFilePath);
                    var lines = content.Split('\n');
                    var results = lines.Select(line => _serializer.FromJson<StepResult>(line)).ToArray();
                    return results.AsQueryable();
                }

                return new StepResult[0].AsQueryable();
            }
        }
    }
}