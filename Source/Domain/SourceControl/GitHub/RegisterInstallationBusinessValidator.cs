/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    /// TODO: should be checking for uniqueness. Don't allow an installation to be re-registered again.

    /// <summary>
    /// A validator for the <see cref="RegisterInstallation" /> command
    /// </summary>
    public class RegisterInstallationBusinessValidator : CommandBusinessValidatorFor<RegisterInstallation>
    {
        // This command is triggered by an external event, so there is no point in trying to validate it
    }
}
