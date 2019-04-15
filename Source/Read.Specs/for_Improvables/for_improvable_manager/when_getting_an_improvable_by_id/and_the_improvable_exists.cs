/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Read.Improvables;

namespace Read.Specs.for_Improvables.for_improvable_manager.when_getting_an_improvable_by_id
{
    [Subject(typeof(ImprovableManager))]
    public class and_the_improvable_exists : given.an_improvables_manager_for<and_the_improvable_exists>
    {
        static Improvable result;
        Establish context = () => and_the_improvable_exists.improvables_file_exists = true;
        Because of = () => result = improvable_manager.GetById(improvable_that_exists);
        It should_return_the_improvable_with_the_requested_id = () => result.ShouldNotBeNull();
    }
}