/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Read.Improvables;

namespace Read.Specs.for_Improvables.for_improvable_manager.when_checking_if_an_improvable_name_is_in_use
{
    [Subject(typeof(ImprovableManager))]
    public class and_the_name_is_used : given.an_improvables_manager_for<and_the_name_is_used>
    {
        static bool name_exists;
        Establish context = () => and_the_name_is_used.improvables_file_exists = true;
        Because of = () => name_exists = improvable_manager.Exists(improvable_name_that_exists);
        It should_be_true = () => name_exists.ShouldBeTrue();
    }
}