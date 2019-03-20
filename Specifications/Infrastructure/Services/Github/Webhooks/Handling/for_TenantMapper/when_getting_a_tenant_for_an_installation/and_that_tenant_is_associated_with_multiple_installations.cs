/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_getting_a_tenant_for_an_installation
{

    [Subject(typeof(IInstallationToTenantMapper), "GetTenantFor")]
    public class and_that_tenant_is_associated_with_multiple_installations : given.a_tenant_mapper
    {
        static TenantId mapped_tenant_for_first;
        static TenantId mapped_tenant_for_second;

        Because of = () => 
        {
            mapped_tenant_for_first = mapper.GetTenantFor(installation_one);
            mapped_tenant_for_second = mapper.GetTenantFor(installation_one);
        };

        It should_return_the_correct_tenant_for_the_first_installation = () => mapped_tenant_for_first.ShouldEqual((TenantId)tenant_with_single_installation); 
        It should_return_the_correct_tenant_for_the_second_installation = () => mapped_tenant_for_first.ShouldEqual((TenantId)tenant_with_single_installation);   
        It should_return_the_same_tenant_for_each_installation = () => mapped_tenant_for_first.ShouldEqual(mapped_tenant_for_second);  
    }
}