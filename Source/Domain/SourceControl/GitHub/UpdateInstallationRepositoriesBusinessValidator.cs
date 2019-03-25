/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Validates the rules associated with an <see cref="UpdateInstallationRepositories" /> command
    /// </summary>
    public class UpdateInstallationRepositoriesBusinessValidator : CommandBusinessValidatorFor<UpdateInstallationRepositories>
    {
        // This command is triggered by an external event, so there is no point in trying to validate it

        //TODO:  we still want to do stuff like check for the existence.  If something is wrong, we want to catch it as early as possible.  
        //Probably not a lot to it though.
    }
}
