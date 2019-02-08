/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Concepts
{
    /// <summary>
    /// Represents the concept of a version in string format
    /// </summary>
    public class VersionString : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from a <see cref="string"/> to <see cref="VersionString"/>
        /// </summary>
        public static implicit operator VersionString(string value)
        {
            return new VersionString {Value = value};
        }
    }
}