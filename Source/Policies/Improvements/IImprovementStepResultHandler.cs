/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;

namespace Policies.Improvements
{
    public interface IImprovementStepResultHandler
    {
        void HandleSuccessfulStep(StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
        void HandleFailedStep(StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
    }
}