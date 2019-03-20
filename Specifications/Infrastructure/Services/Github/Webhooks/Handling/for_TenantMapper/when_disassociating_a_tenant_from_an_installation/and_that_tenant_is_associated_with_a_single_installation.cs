/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Concepts.SourceControl.GitHub;
using Dolittle.Tenancy;
using Machine.Specifications;

namespace Infrastructure.Services.Github.Webhooks.Handling.for_TenantMapper.when_associating_a_tenant_with_an_installation
{
    [Subject(typeof(IInstallationToTenantMapper), "DisassociateTenantFromInstallation")]
    public class and_that_tenant_is_associated_with_a_single_installation : given.a_tenant_mapper
    {
        Because of = () => mapper.DisassociateTenantFromInstallation(installation_one);
        
        It should_remove_the_installation_tenant_mapping = () => mapper.GetTenantFor(installation_one).ShouldEqual(TenantId.Unknown);           
    }
}