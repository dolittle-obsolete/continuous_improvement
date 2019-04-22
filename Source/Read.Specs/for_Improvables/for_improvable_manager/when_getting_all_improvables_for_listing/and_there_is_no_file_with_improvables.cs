/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Read.Improvables;
using Machine.Specifications;
using System.Collections.Generic;

namespace Read.Specs.for_Improvables.for_improvable_manager.when_getting_all_improvables_for_listing
{

    [Subject(typeof(ImprovableManager))]
    public class and_there_is_no_file_with_improvables : given.an_improvables_manager_for<and_there_is_no_file_with_improvables>
    {
        static IEnumerable<ImprovableForListing> results;

        Establish context = () => and_there_is_no_file_with_improvables.improvables_file_exists = false;

        Because of = () => results = improvable_manager.GetAllImprovableForListings();

        It should_have_no_listings = () => results.ShouldBeEmpty();
    }    
}