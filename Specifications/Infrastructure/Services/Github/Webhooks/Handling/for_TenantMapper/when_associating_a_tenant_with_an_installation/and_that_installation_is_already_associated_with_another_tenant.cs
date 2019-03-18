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
    public class and_that_installation_is_already_associated_with_another_tenant : given.a_tenant_mapper
    {
        static IEnumerable<InstallationId> mapped_installations;

        Because of = () =>
        {
            mapper.AssociateTenantWithInstallation(installation_three, tenant_with_single_installation);
            mapped_installations = mapper.GetInstallationsFor(tenant_with_single_installation);
        };

        //What should happen?  Update the installation?  Throw an exception?
    }
}