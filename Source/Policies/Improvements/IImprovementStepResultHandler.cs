/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Tenancy;

namespace Policies.Improvements
{
    public interface IImprovementStepResultHandler
    {
        void HandleSuccessfulStep(TenantId tenant, RecipeType recipeName, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
        void HandleFailedStep(TenantId tenant, RecipeType recipeName, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
    }
}