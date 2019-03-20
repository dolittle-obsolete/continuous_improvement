/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Dolittle.IO;
using Dolittle.Serialization.Json;
using Machine.Specifications;
using Moq;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.given
{
    public class a_tenant_mapper
    {
        protected static IInstallationToTenantMapper mapper;

        protected static Mock<ISerializer> serializer;
        protected static Mock<IFileSystem> file_system;
        protected static string file_contents = "this is the tenants mapped to json";
        protected static ConcurrentDictionary<long, Guid> mapped_tenants;
        protected static Guid tenant_with_single_installation;
        protected static Guid tenant_with_multiple_installations;
        protected static Guid tenant_with_no_installations;
        protected static long installation_one;
        protected static long installation_two;
        protected static long installation_three;
        protected static long installation_that_does_not_exist;

        Establish context = () =>
        {
            tenant_with_no_installations = Guid.NewGuid();
            tenant_with_single_installation = Guid.NewGuid();
            installation_one = 1;
            tenant_with_multiple_installations = Guid.NewGuid();
            installation_two = 2;
            installation_three = 3;
            installation_that_does_not_exist = 100;

            var tenants = new Dictionary<long, Guid>
                { { installation_one, tenant_with_single_installation },
                    { installation_two, tenant_with_multiple_installations },
                    { installation_three, tenant_with_multiple_installations }
                };
            mapped_tenants = new ConcurrentDictionary<long, Guid>(tenants);

            serializer = new Mock<ISerializer>();
            file_system = new Mock<IFileSystem>();
            file_system.Setup(_ => _.ReadAllText(Moq.It.IsAny<string>())).Returns(file_contents);
            serializer.Setup(_ => _.FromJson<ConcurrentDictionary<long, Guid>>(file_contents, null)).Returns(mapped_tenants);
            serializer.Setup(_ => _.ToJson(mapped_tenants, null)).Returns(file_contents);

            mapper = new InstallationToTenantMapper(serializer.Object, file_system.Object);
        };
    }
}