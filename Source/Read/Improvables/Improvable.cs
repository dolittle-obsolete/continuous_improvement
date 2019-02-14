/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Improvables;
using Dolittle.ReadModels;

namespace Read.Improvables
{
    public class Improvable : IReadModel
    {
        public ImprovableId Id {  get; set; }

        public string Name {  get; set; }

        public SourceControl SourceControl {  get; set; }
        public IEnumerable<Recipe> Recipes {  get; set; }
        public IEnumerable<ImprovableId> Cascades {  get; set; }

        public ImprovableStatus Status {  get; set; }
    }
}