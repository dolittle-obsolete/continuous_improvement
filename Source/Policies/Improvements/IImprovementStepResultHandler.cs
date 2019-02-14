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
        void HandleSuccessfulStep(RecipeType recipeType, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
        void HandleFailedStep(RecipeType recipeType, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, VersionString version);
    }
}