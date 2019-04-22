/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands;
using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Validates the business rules associated with an <see cref="UnregisterInstallation" /> command
    /// </summary>
    public class UnregisterInstallationBusinessValidator : CommandBusinessValidatorFor<UnregisterInstallation>
    {
        
    }
}
