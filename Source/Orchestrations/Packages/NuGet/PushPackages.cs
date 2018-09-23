/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Infrastructure.Orchestrations;
using NuGet.Configuration;

namespace Orchestrations.Packages.NuGet
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> for pushing NuGet packages - if there are any
    /// </summary>
    /// <remarks>
    /// Based on:
    /// https://github.com/NuGet/NuGet.Client/blob/dev/src/NuGet.Clients/NuGet.CommandLine/Commands/PushCommand.cs
    /// </remarks>
    public class PushPackages : IPerformer<Context>, INeedConfigurationOf<NuGetConfig>
    {
        /// <inheritdoc/>
        public NuGetConfig Config { get; set; }

        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {

            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task Perform(Context score)
        {
            var settings = new Settings("./");
            var packageSourceProvider = new PackageSourceProvider(settings);

            var repositoriesToPushTo = Config.Repositories.Where(_ => _.CanPush(score));
            Parallel.ForEach(repositoriesToPushTo, async _ =>
            {
                try
                {
                    await global::NuGet.Commands.PushRunner.Run(
                        settings,
                        packageSourceProvider,
                        "path",
                        "source",
                        "apiKey",
                        "symbolSource",
                        "symbolApiKey",
                        3600, // Timeout
                        false, // Disable buffering
                        false, // NoSymbols
                        false, // No Service Endpoint
                        new NuGetLogger());
                }
                catch (TaskCanceledException)
                {
                    // Timeout
                }
                catch (Exception)
                {
                    throw;
                }
            });

            await Task.CompletedTask;
        }
    }
}
