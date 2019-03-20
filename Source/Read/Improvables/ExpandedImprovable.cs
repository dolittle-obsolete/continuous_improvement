/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;

namespace Read.Improvables
{
    /// <summary>
    /// Expands an <see cref="Improvable" /> with additional information
    /// </summary>
    public class ExpandedImprovable
    {
        /// <summary>
        /// Id 
        /// </summary>
        public ImprovableId Id {  get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// Source Control
        /// </summary>
        public SourceControl SourceControl {  get; set; }
        /// <summary>
        /// A list of <see cref="ExpandedRecipe">recipes</see>
        /// </summary>
        public IEnumerable<ExpandedRecipe> Recipes { get; set; }
        /// <summary>
        /// The status of the improvable
        /// </summary>
        public ImprovableStatus Status {  get; set; }
    }
}