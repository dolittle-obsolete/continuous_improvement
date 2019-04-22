/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.SourceControl
{
    /// <summary>
    /// A unique name for a repository, expressed as a string.
    /// </summary>
    public class RepositoryFullName : ConceptAs<string>
    {
        /// <summary>
        /// Represents an Empty or Unset <see cref="RepositoryFullName" />
        /// </summary>
        /// <value></value>
        public static RepositoryFullName Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiates an instance of a <see cref="RepositoryFullName" />
        /// </summary>
        public RepositoryFullName() => Value = string.Empty;

        /// <summary>
        /// Instantiats an instance of a <see cref="RepositoryFullName" />
        /// </summary>
        /// <param name="value">The value to set the name to</param>
        public RepositoryFullName(string value) => Value = value;
        /// <summary>
        /// Implicitly converts a <see cref="string" /> to a <see cref="RepositoryFullName" />
        /// </summary>
        /// <param name="value">The value you wish to convert</param>
        public static implicit operator RepositoryFullName(string value)
        {
            return new RepositoryFullName {Value = value};
        }
    }
}
