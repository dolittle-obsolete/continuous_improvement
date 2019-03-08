/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_getting_a_tenant_for_an_installation
{
    [Subject(typeof(IInstallationToTenantMapper), "GetTenantFor")]
    public class and_that_installation_does_not_exist : given.a_tenant_mapper
    {
        static TenantId mapped_tenant;

        Because of = () => mapped_tenant = mapper.GetTenantFor(installation_that_does_not_exist);

        It should_return_the_unknown_tenant = () => mapped_tenant.ShouldEqual(TenantId.Unknown);
    }
}