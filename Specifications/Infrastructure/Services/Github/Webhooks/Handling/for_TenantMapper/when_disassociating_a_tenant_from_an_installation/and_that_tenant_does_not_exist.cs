/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_associating_a_tenant_with_an_installation
{
    [Subject(typeof(IInstallationToTenantMapper), "DisassociateTenantFromInstallation")]
    public class and_that_installation_does_not_exist : given.a_tenant_mapper
    {
        Because of = () => mapper.DisassociateTenantFromInstallation(100);
        
        //Do nothing or throw?   
        //Should an installation then be associated with the Uknown tenant? Or deleted?     
    }
}