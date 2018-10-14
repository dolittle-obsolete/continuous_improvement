/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_Conductor.given
{
    public class a_conductor : all_dependencies
    {
        protected static Conductor conductor;
        Establish context = () => conductor = new Conductor(container.Object, logger, serializer);
    }


}