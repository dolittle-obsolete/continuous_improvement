/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Concepts.SourceControl
{
    /// <summary>
    /// Represents a type of path within a repository
    /// </summary>
    public class Path : ConceptAs<string>
    {
        /// <summary>
        /// Represents an Empty or Unset <see cref="Path" />
        /// </summary>
        /// <value></value>
        public static Path Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiates an instance of a <see cref="Path" />
        /// </summary>
        public Path() => Value = string.Empty;

        /// <summary>
        /// Instantiats an instance of a <see cref="Path" />
        /// </summary>
        /// <param name="value">The value to set the name to</param>
        public Path(string value) => Value = value;

        /// <summary>
        /// Implicitly convert from <see cref="string"/> to a <see cref="Path"/>
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert from</param>
        public static implicit operator Path(string value) => new Path(value);
    }
}
