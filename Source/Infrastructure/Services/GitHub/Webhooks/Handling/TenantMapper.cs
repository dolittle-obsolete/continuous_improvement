using Dolittle.Tenancy;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public class TenantMapper : ITenantMapper
    {
        public TenantId GetTenantFor(InstallationId installationId)
        {
            // TODO: Implement
            return TenantId.Development;
        }
    }
}