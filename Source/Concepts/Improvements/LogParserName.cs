/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents a type of log parser
    /// </summary>
    public class LogParserName : ConceptAs<string>
    {
        /// <summary>
        /// Represents an Empty or Unset <see cref="LogParserName" />
        /// </summary>
        /// <value></value>
        public static LogParserName Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiats an instance of a <see cref="LogParserName" />
        /// </summary>
        /// <param name="value"></param>
        public LogParserName(string value) => Value = value;

        /// <summary>
        /// Implicitly convert from <see cref="string"/> to a <see cref="LogParserName"/>
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert from</param>
        public static implicit operator LogParserName(string value) => new LogParserName(value);
    }
}
