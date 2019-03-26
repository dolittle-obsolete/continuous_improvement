/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Domain;

namespace Domain.Improvables
{
    /// <summary>
    /// An Improvable. A project that is capable of having improvements applied to it.
    /// </summary>
    public class Improvable : AggregateRoot
    {
        /// <summary>
        /// Instantiates a new instance of an <see cref="Improvable" /> with the specified id.
        /// </summary>
        /// <param name="id">The Id of the Improvable</param>
        public Improvable(Dolittle.Runtime.Events.EventSourceId id) : base(id)
        {
        }
    }
}