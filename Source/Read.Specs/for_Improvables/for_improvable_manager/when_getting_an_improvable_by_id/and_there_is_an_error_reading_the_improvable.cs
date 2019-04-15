/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Machine.Specifications;
using Read.Improvables;

namespace Read.Specs.for_Improvables.for_improvable_manager.when_getting_an_improvable_by_id
{

    [Subject(typeof(ImprovableManager))]
    public class and_there_is_an_error_reading_the_improvable : given.an_improvables_manager_for<and_the_improvable_exists>
    {
        static Exception ex;
        Because of = () => ex = Catch.Exception(() => improvable_manager.GetById(improvable_that_is_unreadable));
        It should_fail = () => ex.ShouldNotBeNull();
        It should_indicate_that_there_was_an_error_reading_the_improvable = () => ex.ShouldBeOfExactType<ErrorReadingImprovable>();
    }    
}