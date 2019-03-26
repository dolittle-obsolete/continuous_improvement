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

namespace Domain.Specs.for_Improvable.when_registering.when_validating.given
{
    public class an_input_validator_for<T>
    {
        protected static RegisterImprovableInputValidator input_validator;

        Establish context = () =>
        {
            input_validator = new RegisterImprovableInputValidator();
        };
    }
}