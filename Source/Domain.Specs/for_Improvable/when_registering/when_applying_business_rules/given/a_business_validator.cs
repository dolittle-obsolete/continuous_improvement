/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using Domain.Improvables;
using Machine.Specifications;

namespace Domain.Specs.for_Improvable.when_registering.when_applying_business_rules.given
{
    public class a_business_validator_for<T> 
    {
        protected static RegisterImprovableBusinessValidator business_validator;
        protected static ImprovableExists improvable_exists = (id) => id == constants.improvable_exists;
        protected static ImprovableNameExists improvable_name_exists = (name) => name == constants.improvable_name_exists;
        protected static RecipeTypeExists recipe_exists = (recipe) => recipe == constants.valid_recipe_type;
        protected static RepositoryExists repository_exists = (repo) => repo == constants.valid_repository;

        Establish context = () => business_validator = new RegisterImprovableBusinessValidator(improvable_exists, improvable_name_exists, recipe_exists, repository_exists);
    }
}