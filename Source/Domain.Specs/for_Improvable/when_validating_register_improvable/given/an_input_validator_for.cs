/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.Improvables;
using Concepts.SourceControl;
using Dolittle.Collections;
using Domain.Improvables;
using Machine.Specifications;

namespace Domain.Specs.for_Improvable.when_validating_register_improvable.given
{
    public class an_input_validator_for<T> 
    {
        protected static RegisterImprovableInputValidator input_validator;
        protected static ImprovableId valid_improvable_id = Guid.NewGuid();
        protected static ImprovableName valid_improvable_name = "MyImprovable";
        protected static RecipeType valid_recipe_type = "AllTheThings!";
        protected static RepositoryFullName valid_repository = "Some Repo on Github";
        protected static Concepts.SourceControl.Path valid_path = "a valid path within the repo";

        Establish context = () => 
        {
            input_validator = new RegisterImprovableInputValidator();
        };

        protected static RegisterImprovable get_command(IEnumerable<Action<RegisterImprovable>> invalidations = null)
        {
            var improvable =  new RegisterImprovable
            {
                Improvable = valid_improvable_id,
                Name = valid_improvable_name,
                Recipe = valid_recipe_type,
                Repository = valid_repository,
                Path = valid_path
            };
            if(invalidations != null)
                invalidations.ForEach(_ => _(improvable));
            return improvable;
        }
    }
}