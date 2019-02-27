/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Domain.Improvements.Metadata;
using Machine.Specifications;

namespace Domain.Specs.for_Improvement.for_metadata.when_building.given
{
    public class a_factory
    {
        protected static ImprovementMetadataFactory factory;

        Establish context = () => factory = new ImprovementMetadataFactory(new ImprovementMetadataValidator());
    }
}
