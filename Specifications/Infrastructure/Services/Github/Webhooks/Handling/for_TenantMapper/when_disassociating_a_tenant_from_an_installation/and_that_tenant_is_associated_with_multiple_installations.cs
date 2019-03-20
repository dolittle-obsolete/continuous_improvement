/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Concepts.SourceControl.GitHub;
using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_associating_a_tenant_with_an_installation
{

    [Subject(typeof(IInstallationToTenantMapper), "DisassociateTenantFromInstallation")]
    public class and_that_tenant_is_associated_with_multiple_installations : given.a_tenant_mapper
    {
        Because of = () => mapper.DisassociateTenantFromInstallation(installation_two);
        
        It should_remove_the_installation_tenant_mapping = () => mapper.GetTenantFor(installation_two).ShouldEqual(TenantId.Unknown); 
        It should_not_remove_any_other_installations_for_that_tenant = () => mapper.GetInstallationsFor(tenant_with_multiple_installations).Any().ShouldBeTrue();  
        It should_update_the_installation_tenant_mapping_file = () => 
        {
            serializer.Verify(_ => _.ToJson(mapped_tenants, null),Moq.Times.Once());
            file_system.Verify(_ => _.WriteAllText(Moq.It.IsAny<string>(),file_contents));
        };           
    }
}