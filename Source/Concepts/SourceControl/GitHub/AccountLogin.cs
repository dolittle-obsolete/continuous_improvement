/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    /// <summary>
    /// Represents login details for an account
    /// </summary>
    public class AccountLogin : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts a <see cref="string" /> to an <see cref="AccountLogin" />
        /// </summary>
        /// <param name="value">The string you wish to convert</param>
        public static implicit operator AccountLogin(string value)
        {
            return new AccountLogin {Value = value};
        }
    }
}
