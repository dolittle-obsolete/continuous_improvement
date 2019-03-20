/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_getting_a_tenant_for_an_installation
{

    [Subject(typeof(IInstallationToTenantMapper), "GetTenantFor")]
    public class and_that_tenant_is_associated_with_a_single_installation : given.a_tenant_mapper
    {
        static TenantId mapped_tenant;

        Because of = () => mapped_tenant = mapper.GetTenantFor(installation_one);

        It should_return_the_correct_tenant = () => mapped_tenant.ShouldEqual((TenantId)tenant_with_single_installation);        
    }
}