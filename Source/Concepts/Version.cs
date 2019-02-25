/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts
{
    /// <summary>
    /// Represents a semantic <see cref="Version" />
    /// </summary>
    public class Version : ConceptAs<string>
    {
        /// <summary>
        ///The state of a <see cref="Version" /> that has not been set.
        /// </summary>
        public static Version Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiate a <see cref="Version" /> with the Empty state
        /// </summary>
        /// <returns></returns>
        public Version() : this(string.Empty)
        {}

        /// <summary>
        /// Instantiate a <see cref="Version" /> with a version number
        /// </summary>
        /// <param name="value"></param>
        public Version(string value) => Value = value;

        /// <summary>
        /// Implicitly convert a string to a <see cref="Version" />
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Version(string value) => new Version(value);

        /// <summary>
        /// Creates a valid Version from the component parts
        /// </summary>
        /// <param name="major">The Major version</param>
        /// <param name="minor">The Minor version</param>
        /// <param name="patch">The Patch version</param>
        /// <param name="label">An optional label</param>
        /// <returns></returns>
        public Version From(short major, short minor, short patch, string label = null)
        {
            var postfix = string.IsNullOrWhiteSpace(label) ? string.Empty : $"-{label.Trim()}";
            return $"{major}.{minor}.{patch}{postfix}";
        }
    }
}
