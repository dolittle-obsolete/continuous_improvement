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
    public class and_a_file_with_improvables_for_listing_exists : given.an_improvables_manager_for<and_a_file_with_improvables_for_listing_exists>
    {
        static IEnumerable<ImprovableForListing> results;

        Establish context = () => and_a_file_with_improvables_for_listing_exists.improvables_file_exists = true;

        Because of = () => results = improvable_manager.GetAllImprovableForListings();

        It should_have_all_the_listings = () => results.ShouldContainOnly(improvables_for_listing);
    }
}