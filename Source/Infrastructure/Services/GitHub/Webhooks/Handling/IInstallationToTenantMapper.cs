/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using Dolittle.Tenancy;
using Concepts.SourceControl.GitHub;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <summary>
    /// Defines a mapper from installation to tenants
    /// </summary>
    public interface IInstallationToTenantMapper
    {
        /// <summary>
        /// Gets the <see cref="TenantId">Tenant</see> associated with this <see cref="InstallationId" />
        /// </summary>
        /// <param name="installationId">The installation to get the tenant for</param>
        /// <returns><see cref="TenantId">Tenant</see></returns>
        TenantId GetTenantFor(InstallationId installationId);
        /// <summary>
        /// Associates a Tenant with an Installation
        /// </summary>
        /// <param name="installationId">The <see cref="InstallationId" /> to associate to the <see cref="TenantId">Tenant</see></param>
        /// <param name="tenantId">The <see cref="TenantId">Tenant</see> to associate to the <see cref="InstallationId" /></param>
        void AssociateTenantWithInstallation(InstallationId installationId, TenantId tenantId);
        /// <summary>
        /// Removes the association between the <see cref="TenantId">Tenant</see> and <see cref="InstallationId">Installation</see>
        /// </summary>
        /// <param name="installationId"></param>
        void DisassociateTenantFromInstallation(InstallationId installationId);
        /// <summary>
        /// Gets all the <see cref="InstallationId">installations</see> associated with the <see cref="TenantId">Tenant</see>
        /// </summary>
        /// <param name="tenant">The tenant to get the installations for</param>        
        IEnumerable<InstallationId> GetInstallationsFor(TenantId tenant);
    }
}