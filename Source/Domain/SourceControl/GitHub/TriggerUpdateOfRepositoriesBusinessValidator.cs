/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Validates the business rules associated with a <see cref="TriggerUpdateOfRepositories" /> command
    /// </summary>
    public class TriggerUpdateOfRepositoriesBusinessValidator : CommandBusinessValidatorFor<TriggerUpdateOfRepositories>
    {
        
    }
}
