/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.IO;
using Orchestrations.Build;
using Orchestrations;
using Dolittle.Hosting;
using Dolittle.Serialization.Json;
using Read.Configuration;
using Dolittle.DependencyInversion;
using Dolittle.Assemblies;
using Microsoft.Extensions.Logging;
using Dolittle.Logging;
using Orchestrations.SourceControl;

namespace EntryPoint
{

    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static IContainer Container;

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
            Container = result.Container;

            CreateWebHostBuilder(args).Build().Run();

            /*
            
                var serializer = result.Container.Get<ISerializer>();

                var projectAsJson = File.ReadAllText("./Builds/508c1745-5f2a-4b4c-b7a5-2fbb1484346d/f1d0d79d-d47e-4b56-9663-d8c11fe3a9f4/configuration.json");
                var project = serializer.FromJson<Project>(projectAsJson);
                var buildNumberFile = "./Builds/508c1745-5f2a-4b4c-b7a5-2fbb1484346d/f1d0d79d-d47e-4b56-9663-d8c11fe3a9f4/buildNumber";
                var buildNumberAsText = File.ReadAllText(buildNumberFile);
                var buildNumber = int.Parse(buildNumberAsText)+1;
                File.WriteAllText(buildNumberFile, buildNumber.ToString());

                var source = Path.Combine(Directory.GetCurrentDirectory(),"Builds","508c1745-5f2a-4b4c-b7a5-2fbb1484346d","f1d0d79d-d47e-4b56-9663-d8c11fe3a9f4","source");

                var context = new Context(project, source, "beb7544a44dff9283ba2f1d5c3cc8a567dfffa6c", buildNumber, false);
                var score = new ScoreOf<Context>(context);
                score.AddStep(new GetLatest());
                score.AddStep(new GetVersion());
                score.AddStep(new CompileAndPackage());

             */
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
