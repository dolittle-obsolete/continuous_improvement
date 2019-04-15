/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Concepts.Improvables;
using Concepts.SourceControl;
using Dolittle.Domain;
using Events.Improvables;

namespace Domain.Improvables
{
    /// <summary>
    /// An Improvable. A project that is capable of having improvements applied to it.
    /// </summary>
    public class Improvable : AggregateRoot
    {   
        private bool _registered; 

        /// <summary>
        /// Instantiates a new instance of an <see cref="Improvable" /> with the specified id.
        /// </summary>
        /// <param name="id">The Id of the Improvable</param>
        public Improvable(Dolittle.Runtime.Events.EventSourceId id) : base(id)
        {
            _registered = false;
        }

        /// <summary>
        /// Registers a new <see cref="Improvable" />
        /// </summary>
        /// <param name="name">The name of the new improvable</param>
        /// <param name="recipe">The recipe type to associate with the new improvable</param>
        /// <param name="repository">The source control repository associated with the improvable</param>
        /// <param name="path">The path within the Source Control repository to the building artifact</param>
        public void Register(ImprovableName name, RecipeType recipe, RepositoryFullName repository, Path path)
        {
            if(_registered)
                throw new ImprovableAlreadyRegistered($"An improvable with the Id '{this.EventSourceId.Value}' has already been registered");
            Apply(new ImprovableRegistered(this.EventSourceId,name,recipe,repository,path));
        }

        private void On(ImprovableRegistered @event)
        {
            _registered = true;
        }
    }
}