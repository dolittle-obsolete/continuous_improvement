/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;

namespace Domain.Improvables
{
    /// <summary>
    /// Validates that business rules associated with a <see cref="RegisterImprovable" /> command are satisfied
    /// </summary>
    public class RegisterImprovableBusinessValidator : CommandBusinessValidatorFor<RegisterImprovable>
    {
        
    }
}
