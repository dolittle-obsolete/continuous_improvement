/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;

namespace Read.Improvables
{
    public class ExpandedImprovable
    {
        public ImprovableId Id {  get; set; }
        public string Name {  get; set; }
        public SourceControl SourceControl {  get; set; }

        public IEnumerable<ExpandedRecipe> Recipes { get; set; }

        public ImprovableStatus Status {  get; set; }
    }
}