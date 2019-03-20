/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_getting_installations_for_a_tenant
{
    [Subject(typeof(IInstallationToTenantMapper), "GetInstallationsFor")]
    public class and_that_tenant_does_not_exist : given.a_tenant_mapper
    {
        static IEnumerable<InstallationId> mapped_installations;
        Because of = () => mapped_installations = mapper.GetInstallationsFor(tenant_with_no_installations);

        It should_return_no_installations = () => mapped_installations.ShouldBeEmpty();
    }
}