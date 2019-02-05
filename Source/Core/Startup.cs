/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Autofac;
using Dolittle.AspNetCore.Bootstrap;
using Dolittle.DependencyInversion.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orchestrations.Build;
using Swashbuckle.AspNetCore.Swagger;
using Orchestrations.Triggers;
using Infrastructure.Routing;
using Dolittle.Booting;
using System.Net.Http;
using System;
using Core.SourceControl.GitHub;
using Infrastructure.Services.Github.Webhooks;
using Infrastructure.Services.Github.UserAuthentication;
using Infrastructure.Services.Github.Installation;

namespace Core
{
    /// <summary>
    /// Startup for ASP.NET Core
    /// </summary>
    public partial class Startup
    {
        readonly IHostingEnvironment _hostingEnvironment;
        readonly ILoggerFactory _loggerFactory;
        BootloaderResult _bootResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="loggerFactory"></param>
        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(10);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddMvc();

            _bootResult = services.AddDolittle(_loggerFactory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDolittle(_bootResult.Assemblies, _bootResult.Bindings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

                app.UseCors(builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            //app.UseGitHubTrigger();
            app.UseGitHubWebhookHandler();
            app.UseGitHubUserAuthentication();
            app.UseGitHubInstallationHandler();

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapPost<BuildJobDone>(app, $"buildJobDone");
            app.UseRouter(routeBuilder.Build());

            app.UseDolittle();

            /*
            app.Run(async context => {
                Console.WriteLine($"Proxying request!");
                var proxyClient = new HttpClient();

                var proxyUri = new UriBuilder(context.Request.Scheme, "localhost", 8080, context.Request.Path, context.Request.QueryString.ToUriComponent()).Uri;
                var proxyResult = await proxyClient.GetAsync(proxyUri);

                context.Response.StatusCode = (int)proxyResult.StatusCode;

                using (var proxyStream = await proxyResult.Content.ReadAsStreamAsync())
                {
                    await proxyStream.CopyToAsync(context.Response.Body);
                }
            });
            */

            app.RunAsSinglePageApplication();
        }
    }
}