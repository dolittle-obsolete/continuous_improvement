using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.Installation
{
    public static class Extensions
    {
        public static void UseGitHubInstallationHandler(this IApplicationBuilder app, string path = "thirdparty/github/installation/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet<Setup>(app, path+"setup");
            app.UseRouter(routeBuilder.Build());
        }
    }
}