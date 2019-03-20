/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents the Severity of the Step Result
    /// </summary>
    public enum StepResultSeverity
    {
        
        /// <summary>
        /// The Step Result had a warning
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The Step Result has an Error
        /// </summary>
        Error,
        
        /// <summary>
        /// The Step Result has information messages
        /// </summary>
        Info
    }
}