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

        Establish context = () => business_validator = new RegisterImprovableBusinessValidator(improvable_exists);
    }
}