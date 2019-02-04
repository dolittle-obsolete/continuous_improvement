using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.UserAuthentication
{
    public static class Extensions
    {
        public static void UseGitHubUserAuthentication(this IApplicationBuilder app, string path = "thirdparty/github/userauth/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet<Authenticate>(app, path+"initiate");
            routeBuilder.MapGet<Callback>(app, path+"callback");
            routeBuilder.MapGet<GetInstallationsForUserProxy>(app, path+"installations");
            app.UseRouter(routeBuilder.Build());
        }
    }
}