/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Moq;
using Machine.Specifications;
using Dolittle.Logging;
using Dolittle.DependencyInversion;
using Dolittle.Serialization.Json;

namespace Infrastructure.Orchestrations.for_Conductor.given
{
    public class all_dependencies
    {
        protected static Mock<IContainer> container;
        protected static ILogger logger;
        protected static ISerializer serializer;

        Establish context = () => 
        {
            logger = Mock.Of<ILogger>();
            container = new Mock<IContainer>();
            serializer = Mock.Of<ISerializer>();
        };
    }
}