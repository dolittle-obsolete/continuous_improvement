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
    //TODO:  this long list of parameters obviously represents something.  Should capture what that something is.

    /// <summary>
    /// Defines the result handler for an improvment step
    /// </summary>
    public interface IImprovementStepResultHandler
    {
        /// <summary>
        /// Handles a successful step
        /// </summary>
        /// <param name="recipeType">The Recipe Type of the successful step</param>
        /// <param name="stepNumber">The number of the successful step</param>
        /// <param name="improvement">The improvement of the successful step</param>
        /// <param name="improvable">The improvable of the successful step</param>
        /// <param name="version">The version of the successful step</param>
        void HandleSuccessfulStep(RecipeType recipeType, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, Version version);
        /// <summary>
        /// Handles a failed step
        /// </summary>
        /// <param name="recipeType">The Recipe Type of the failed step</param>
        /// <param name="stepNumber">The number of the failed step</param>
        /// <param name="improvement">The improvement of the failed step</param>
        /// <param name="improvable">The improvable of the failed step</param>
        /// <param name="version">The version of the failed step</param>        
        void HandleFailedStep(RecipeType recipeType, StepNumber stepNumber, ImprovementId improvement, ImprovableId improvable, Version version);
    }
}