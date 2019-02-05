using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Github.Installation
{
    public interface ICanHandleInstallationCallbacks
    {
        void Install(long installationId, HttpResponse response);

        void Update(long installationId, HttpResponse response);
    }
}