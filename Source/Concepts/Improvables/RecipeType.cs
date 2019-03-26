/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Concepts.Improvables
{
    /// <summary>
    /// Represents the unique identifier for a recipe type in the system
    /// </summary>
    public class RecipeType : ConceptAs<string>
    {
        /// <summary>
        /// Represents an Empty or Unset <see cref="RecipeType" />
        /// </summary>
        /// <value></value>
        public static RecipeType Empty { get; } = string.Empty;
        /// <summary>
        /// Instantiates an instance of a <see cref="RecipeType" />
        /// </summary>
        public RecipeType() => Value = string.Empty;

        /// <summary>
        /// Instantiats an instance of a <see cref="RecipeType" />
        /// </summary>
        /// <param name="value">The value to set the name to</param>
        public RecipeType(string value) => Value = value;
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="RecipeType"/>
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert from</param>
        public static implicit operator RecipeType(string value)
        {
            return new RecipeType { Value = value };
        }
    }
}