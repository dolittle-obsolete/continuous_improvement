/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    /// <summary>
    /// Repsents the name of a repository. Expressed as a string.
    /// </summary>
    public class RepositoryFullName : ConceptAs<string>
    {
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
