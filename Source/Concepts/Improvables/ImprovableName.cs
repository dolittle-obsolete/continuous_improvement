/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Concepts.Improvables
{
    /// <summary>
    /// Represents a type of improvable name
    /// </summary>
    public class ImprovableName : ConceptAs<string>
    {
        /// <summary>
        /// Represents an Empty or Unset <see cref="ImprovableName" />
        /// </summary>
        /// <value></value>
        public static ImprovableName Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiates an instance of a <see cref="ImprovableName" />
        /// </summary>
        public ImprovableName() => Value = string.Empty;

        /// <summary>
        /// Instantiats an instance of a <see cref="ImprovableName" />
        /// </summary>
        /// <param name="value">The value to set the name to</param>
        public ImprovableName(string value) => Value = value;

        /// <summary>
        /// Implicitly convert from <see cref="string"/> to a <see cref="ImprovableName"/>
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert from</param>
        public static implicit operator ImprovableName(string value) => new ImprovableName(value);
    }
}
