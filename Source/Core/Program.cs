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
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace Core
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
            

            //var reader = new PemReader(new TextReader() );

/*
            var c = new X509Certificate2("/Users/jakob/Secrets/continuous-improvement.private-key.pem");
            System.Console.WriteLine($"Have something? {c}");
        */

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
