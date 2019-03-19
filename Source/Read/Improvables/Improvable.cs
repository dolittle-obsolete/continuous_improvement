/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;
using Dolittle.ReadModels;

namespace Read.Improvables
{
    /// <summary>
    /// Represents an Improvable
    /// </summary>
    public class Improvable : IReadModel
    {
        /// <summary>
        /// The Id of the improvable
        /// </summary>
        public ImprovableId Id {  get; set; }
        /// <summary>
        /// The name of the improvable
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// Source control details associated with this improvable
        /// </summary>
        /// <value></value>
        public SourceControl SourceControl {  get; set; }
        /// <summary>
        /// A colletion of <see cref="Recipe">recipes</see> associated with this improvable
        /// </summary>
        /// <value></value>
        public IEnumerable<Recipe> Recipes {  get; set; }
        /// <summary>
        /// A list of other <see cref="ImprovableId">improvables</see> that changes to this improvable cascades to
        /// </summary>
        public IEnumerable<ImprovableId> Cascades {  get; set; }
        /// <summary>
        /// The status of the improvable
        /// </summary>
        /// <value></value>
        public ImprovableStatus Status {  get; set; }
    }
}