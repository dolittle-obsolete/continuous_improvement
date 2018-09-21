/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.IO;
using Dolittle.DependencyInversion;
using Dolittle.Assemblies;
using Microsoft.Extensions.Logging;
using Dolittle.Logging;
using Infrastructure.Routing;

namespace EntryPoint
{

    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main - start method
        /// </summary>
        /// <param name="args">Arguments for the process</param>
        public static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory();
            var logAppenders = Dolittle.Logging.Bootstrap.EntryPoint.Initialize(loggerFactory);
            var logger = new Logger(logAppenders);

            var assemblies = Dolittle.Assemblies.Bootstrap.EntryPoint.Initialize(logger);
            var typeFinder = Dolittle.Types.Bootstrap.EntryPoint.Initialize(assemblies);


            var bindings = new[] {
                new BindingBuilder(Binding.For(typeof(IAssemblies))).To(assemblies).Build(),
                new BindingBuilder(Binding.For(typeof(Dolittle.Logging.ILogger))).To(logger).Build()
            };

            var result = Dolittle.DependencyInversion.Bootstrap.Boot.Start(assemblies, typeFinder, logger, bindings);
            RouteBuilderExtensions.Container = result.Container;

            CreateWebHostBuilder(args).Build().Run();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
