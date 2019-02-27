/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Dolittle.Domain;
using Dolittle.Runtime.Events;
using Events.Improvements;

namespace Domain.Improvements
{
    /// <summary>
    /// Represents an individual improvement
    /// </summary>
    public class Improvement : AggregateRoot
    {
        ImprovableId _for = ImprovableId.Empty;

        bool IsInitiated => !_for?.IsEmpty() ?? false;
        bool _hasCompleted = false;
        bool _hasFailed = false;

        /// <inheritdoc/>
        public Improvement(EventSourceId id) : base(id) 
        {}

        /// <summary>
        /// Initiates a new improvement for the specified improvable and version
        /// </summary>
        /// <param name="improvable">The improvable being improved</param>
        /// <param name="version">the version number of this improvement</param>
        /// <param name="isFromPullRequest">A flag indicating whether this improvement came from a pull request or not</param>
        public void Initiate(ImprovableId improvable,Version version, bool isFromPullRequest)
        {
            ThrowIfAlreadyInitialized();
            Apply(new ImprovementInitiated(improvable,version,isFromPullRequest));
        }

        /// <summary>
        /// Marks this improvment as complete
        /// </summary>
        public void Complete()
        {
            ThrowIfNotInitialized();
            ThrowIfFinishedAndCannotSetState();
            Apply(new ImprovementCompleted(_for));
        }

        /// <summary>
        /// Marks this improvemnt as Failed
        /// </summary>
        public void Fail()
        {
            ThrowIfNotInitialized();
            ThrowIfFinishedAndCannotSetState();
            Apply(new ImprovementFailed(_for));
        }

        void On(ImprovementInitiated @event)
        {
            _for = @event.ForImprovable;
        }

        void On(ImprovementCompleted @event)
        {
            _hasCompleted = true;
        }

        void On(ImprovementFailed @event)
        {
            _hasFailed = true;
        }

        private void ThrowIfAlreadyInitialized()
        {
            if(IsInitiated)
                throw new ImprovementAlreadyInitiated($"Improvement '{EventSourceId}' is already initiated as an improvement for '{_for.Value}'");
        }

        private void ThrowIfAlreadyCompleted()
        {
            if(_hasCompleted)
                throw new ImprovementAlreadyCompleted($"Improvement '{EventSourceId}' has already successfully completed.");
        }

        private void ThrowIfAlreadyFailed()
        {
            if(_hasFailed)
                throw new ImprovementAlreadyFailed($"Improvement '{EventSourceId}' has already failed.");
        }

        private void ThrowIfFinishedAndCannotSetState()
        {
            ThrowIfAlreadyCompleted();
            ThrowIfAlreadyFailed();
        }

        private void ThrowIfNotInitialized()
        {
            if(!IsInitiated)
                throw new ImprovementNotInitiated($"Improvement '{EventSourceId}' has not been initiated.");
        }
    }
}