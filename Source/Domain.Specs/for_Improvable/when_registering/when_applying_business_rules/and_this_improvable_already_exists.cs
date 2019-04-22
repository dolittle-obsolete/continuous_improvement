/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Validation;
using Domain.Improvables;
using Machine.Specifications;

namespace Domain.Specs.for_Improvable.when_registering.when_applying_business_rules
{
    [Subject(typeof(RegisterImprovableBusinessValidator))]
    public class and_this_improvable_is_already_registered : given.a_business_validator_for<and_this_improvable_is_already_registered>
    {
        static RegisterImprovable register_improvable;
        static IEnumerable<ValidationResult> results;

        Establish context = () => register_improvable = constants.get_command((_)=> _.Improvable = constants.improvable_exists);

        Because of = () => results = business_validator.ValidateFor(register_improvable);

        It should_be_invalid = () => results.ShouldBeInvalid();
        It should_indicate_the_improvable_already_exists = () => results.Any(_ => _.ErrorMessage.Contains("already registered")).ShouldBeTrue();
    }
}