/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Dolittle.Collections;
using Dolittle.Logging;

namespace Infrastructure
{
    /// <summary>
    /// Basic console logger implemenation of <see cref="ILogger" />
    /// TODO: we probably need a testing ILogger implementation as part of the testing helpers
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private HashSet<LogLevel> _logLevels = new HashSet<LogLevel>();

        /// <inheritdoc />
        public void Critical(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Critical,message);
        }

        /// <inheritdoc />
        public void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Debug,message);
        }

        /// <inheritdoc />
        public void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Error,message);
        }
       /// <inheritdoc />
        public void Error(Exception exception, string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Error,message, exception);
        }
        /// <inheritdoc />
        public void Information(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Info,message);
        }
        /// <inheritdoc />
        public void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Trace,message);
        }
        /// <inheritdoc />
        public void Warning(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(LogLevel.Warning,message);
        }

        void Output(LogLevel level, string message, Exception ex = null)
        {
            if(!_logLevels.Contains(level))
                return;
            Console.WriteLine($"{DateTime.UtcNow.ToString()} {level} {message} {ex?.ToString() ?? ""}");
        }

        public void Enable(params LogLevel[] levels)
        {
            levels.Distinct().ForEach(_ => _logLevels.Add(_));
        }
    }
}