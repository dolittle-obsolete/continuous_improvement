/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Serialization.Json;

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Represents an implementation of <see cref="IPerformerLog"/>
    /// </summary>
    public class PerformerLog : IPerformerLog
    {
        readonly string _outputPath;
        readonly int _performerId;
        readonly string _performerName;
        private readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="PerformerLog"/>
        /// </summary>
        /// <param name="outputPath">The output path</param>
        /// <param name="performerId">The incremental key identifier for the <see cref="IPerformer{T}"/></param>
        /// <param name="performerName">The name of the <see cref="IPerformer{T}"/></param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serializing to JSON</param>
        public PerformerLog(
            string outputPath,
            int performerId,
            string performerName,
            ISerializer serializer)
        {
            _outputPath = outputPath;
            _performerId = performerId;
            _performerName = performerName;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public void Error(string message, string source = "", int line = 0)
        {
            var logFile = GetLogFilename("errors");
            Log(logFile, message, source, line);
        }

        /// <inheritdoc/>
        public void Information(string message, string source = "", int line = 0)
        {
            var logFile = GetLogFilename("information");
            Log(logFile, message, source, line);
        }

        /// <inheritdoc/>
        public void Warning(string message, string source = "", int line = 0)
        {
            var logFile = GetLogFilename("warnings");
            Log(logFile, message, source, line);
        }

        string GetLogFilename(string type)
        {
            return Path.Combine(_outputPath, $"{_performerId}.{_performerName}.{type}");
        }

        void Log(string logFile, string message, string source = "", int line = 0)
        {
            var logMessage = new PerformerLogMessage
            {
            Time = DateTimeOffset.UtcNow,
            Message = message,
            Source = source,
            Line = line
            };
            var json = _serializer.ToJson(logMessage);
            File.AppendAllText(logFile, $"{json}\n");
        }
    }
}