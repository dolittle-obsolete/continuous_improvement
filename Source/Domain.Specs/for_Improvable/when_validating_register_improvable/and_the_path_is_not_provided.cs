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
using Dolittle.Validation;
using Domain.Improvables;
using Machine.Specifications;

namespace Domain.Specs.for_Improvable.when_validating_register_improvable
{

    [Subject(typeof(RegisterImprovableInputValidator))]
    public class and_the_path_is_not_provided : given.an_input_validator_for<and_the_repository_name_is_invalid>
    {
        protected static RegisterImprovable register;
        protected static IEnumerable<ValidationResult> results;

        Establish context = () => 
        {
            Action<RegisterImprovable> invalidation = _ => _.Path = null;
            register = get_command(new []{ invalidation});
        };

        Because of = () => results = input_validator.ValidateFor(register);

        It should_be_valid = () => results.Any().ShouldBeFalse();
    }              
}