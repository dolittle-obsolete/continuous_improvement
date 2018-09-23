/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using NuGet.Common;

namespace Orchestrations.Packages.NuGet
{
    /// <summary>
    /// Represents an implementation of <see cref="global::NuGet.Common.ILogger"/>
    /// </summary>
    public class NuGetLogger : global::NuGet.Common.ILogger
    {
        /// <inheritdoc/>
        public void Log(LogLevel level, string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Log(ILogMessage message)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task LogAsync(LogLevel level, string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task LogAsync(ILogMessage message)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogDebug(string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogError(string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogInformation(string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogInformationSummary(string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogMinimal(string data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LogVerbose(string data)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public void LogWarning(string data)
        {
            throw new NotImplementedException();
        }
    }
}