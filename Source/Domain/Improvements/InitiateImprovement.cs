/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Commands;

namespace Domain.Improvements
{
    /// <summary>
    /// A <see cref="ICommand" /> to initiate an improvement
    /// </summary>
    public class InitiateImprovement : ICommand
    {
        /// <summary>
        /// Gets and Sets the <see cref="ImprovementId" />
        /// </summary>
        public ImprovementId Improvement { get; set; }
        /// <summary>
        /// Gets and Sets the <see cref="ImprovableId">Improvable</see> this improvement is for
        /// </summary>
        public ImprovableId ForImprovable { get; set; }
        /// <summary>
        /// Gets and Sets the <see cref="Version" /> of this improvement
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// Indicates whether or not the improvement was initiated from a code pull request
        /// </summary>
        public bool IsFromPullRequest { get; set; }
    }
}
