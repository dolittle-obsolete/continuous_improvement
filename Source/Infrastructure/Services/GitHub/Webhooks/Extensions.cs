using Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure.Services.Github.Webhooks
{
    public static class Extensions
    {
        public static void UseGitHubWebhookHandler(this IApplicationBuilder app, string path = "thirdparty/github/webhooks/")
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapPost<Route>(app, path);
            app.UseRouter(routeBuilder.Build());
        }
    }
}