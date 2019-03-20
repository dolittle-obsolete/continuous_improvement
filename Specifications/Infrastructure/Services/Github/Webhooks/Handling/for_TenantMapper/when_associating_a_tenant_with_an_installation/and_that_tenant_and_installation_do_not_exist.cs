/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_associating_a_tenant_with_an_installation
{
    [Subject(typeof(IInstallationToTenantMapper), "AssociateTenantWithInstallation")]
    public class and_that_tenant_and_installation_do_not_exist : given.a_tenant_mapper
    {
        static InstallationId new_installation;
        static TenantId new_tenant;
        static IEnumerable<InstallationId> mapped_installations;

        Establish context = () =>
        {
            new_installation = 10;
        };

        Because of = () =>
        {
            new_tenant = Guid.NewGuid();
            mapper.AssociateTenantWithInstallation(new_installation, new_tenant);
            mapped_installations = mapper.GetInstallationsFor(new_tenant);
        };

        It should_add_a_second_installation_mapping = () =>
        {
            mapped_installations.Count().ShouldEqual(1);
            mapped_installations.First().ShouldEqual(new_installation);
        };

        It should_update_the_installation_tenant_mapping_file = () =>
        {
            serializer.Verify(_ => _.ToJson(mapped_tenants, null), Moq.Times.Once());
            file_system.Verify(_ => _.WriteAllText(Moq.It.IsAny<string>(), file_contents));
        };
    }
}