using System.Collections.Generic;
using Dolittle.Tenancy;
using Concepts.SourceControl.GitHub;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface IInstallationToTenantMapper
    {
        TenantId GetTenantFor(InstallationId installationId);
        void AssociateTenantWithInstallation(InstallationId installationId, TenantId tenantId);
        void DisassociateTenantFromInstallation(InstallationId installationId);
        IEnumerable<InstallationId> GetInstallationsFor(TenantId tenant);
    }
}