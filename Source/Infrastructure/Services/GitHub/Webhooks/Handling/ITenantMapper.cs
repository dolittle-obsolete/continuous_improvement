using Dolittle.Tenancy;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    public interface ITenantMapper
    {
        TenantId GetTenantFor(InstallationId installationId);
    }
}