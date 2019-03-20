/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    /// <summary>
    /// A unique id for an Installation.  Expressed as a <see cref="long" />
    /// </summary>
    public class InstallationId : ConceptAs<long>
    {
        /// <summary>
        /// Implicitly converts a <see cref="long" /> to an <see cref="InstallationId" />
        /// </summary>
        /// <param name="value">The value you wish to convert</param>
        public static implicit operator InstallationId(long value)
        {
            return new InstallationId {Value = value};
        }
    }
}
