/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    /// <summary>
    /// Validates that a <see cref="TriggerUpdateOfRepositories"/> command is well formed.
    /// </summary>
    public class TriggerUpdateOfRepositoriesInputValidator : CommandInputValidatorFor<TriggerUpdateOfRepositories>
    {
        
    }
}
