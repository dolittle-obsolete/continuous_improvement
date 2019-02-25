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
    /// 
    /// </summary>
    public class Improvement : AggregateRoot
    {
        ImprovableId _for;

        /// <inheritdoc/>
        public Improvement(EventSourceId id) : base(id) { }

        public void Initiate(ImprovableId improvable,Version version, bool isFromPullRequest)
        {
            Apply(new ImprovementInitiated(improvable,version,isFromPullRequest));
        }

        public void Complete()
        {
            Apply(new ImprovementCompleted(_for));
        }

        public void Fail()
        {
            Apply(new ImprovementFailed(_for));
        }

        void On(ImprovementInitiated @event)
        {
            _for = @event.ForImprovable;
        }

    }
}