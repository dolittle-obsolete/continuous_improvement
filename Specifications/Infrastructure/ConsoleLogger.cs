/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.CompilerServices;
using Dolittle.Logging;

namespace Infrastructure
{
    /// <summary>
    /// Basic console logger implemenation of <see cref="ILogger" />
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <inheritdoc />
        public void Critical(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Critical),message);
        }

        /// <inheritdoc />
        public void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Debug),message);
        }

        /// <inheritdoc />
        public void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Error),message);
        }
       /// <inheritdoc />
        public void Error(Exception exception, string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Error),message, exception);
        }
        /// <inheritdoc />
        public void Information(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Information),message);
        }
        /// <inheritdoc />
        public void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Trace),message);
        }
        /// <inheritdoc />
        public void Warning(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Output(nameof(Warning),message);
        }

        void Output(string label, string message, Exception ex = null)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString()} {label} {message} {ex?.ToString() ?? ""}");
        }
    }
}