/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    /// <summary>
    /// Records that an Improvement was Initiated
    /// </summary>
    public class ImprovementInitiated : IEvent
    {
        /// <summary>
        /// Instantiates a new instance of <see cref="ImprovementInitiated" />
        /// </summary>
        /// <param name="forImprovable">The Id of the Improvable that this Improvement is for</param>
        /// <param name="version">The Version that this Improvement is related to</param>
        /// <param name="pullRequest">A flag indicating whether this improvement was initiated from a pull request (true)</param>
        public ImprovementInitiated(Guid forImprovable, string version, bool pullRequest) 
        {
            ForImprovable = forImprovable;
            Version = version;
            PullRequest = pullRequest;
        }

        /// <summary>
        /// The Id of the Improvable that this Improvement is for
        /// </summary>
        public Guid ForImprovable { get; }
        /// <summary>
        /// The Version that this Improvement is related to
        /// </summary>
        public string Version {  get; }
        /// <summary>
        /// A flag indicating whether this improvement was initiated from a pull request (true)
        /// </summary>
        public bool PullRequest { get; }
    }
}