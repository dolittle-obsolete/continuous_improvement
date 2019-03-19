/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    /// <summary>
    /// Represents as Account Type. Expressed as a <see cref="string" />
    /// </summary>
    public class AccountType : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts a <see cref="string" /> to an <see cref="AccountType" />
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator AccountType(string value)
        {
            return new AccountType {Value = value};
        }
    }
}
