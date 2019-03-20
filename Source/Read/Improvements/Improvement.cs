/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.ReadModels;

namespace Read.Improvements
{
    /// <summary>
    /// Represents an improvement
    /// </summary>
    public class Improvement : IReadModel
    {

        /// <summary>
        /// The Id of the Improvement
        /// </summary>
        public ImprovementId Id { get; set; }

        /// <summary>
        /// The Improvable being improved
        /// </summary>
        public ImprovableId Improvable { get; set; }

        /// <summary>
        /// Flag indicating if the improvement was the result of a pull request
        /// </summary>
        /// <value></value>
        public bool PullRequest { get; set; }

        /// <summary>
        /// Gets or sets the time that this improvement succeeded
        /// </summary>
        public DateTimeOffset? Completed { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the time that this improvement failed
        /// </summary>
        public DateTimeOffset? Failed { get; set; }

        /// <summary>
        /// Indicates if the improvement has completed or not
        /// </summary>
        public bool HasCompleted => Completed.HasValue && Completed >= DateTimeOffset.MinValue;
        /// <summary>
        /// Indicates if the improvment has failed or not
        /// </summary>
        public bool HasFailed => Failed.HasValue && Failed >= DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the <see cref="Version">version</see>
        /// </summary>
        public Concepts.Version Version { get; set; }
    }
}