/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts.Projects;
using Dolittle.ReadModels;

namespace Read.Projects
{
    /// <summary>
    /// Represents a single result in a step
    /// </summary>
    public class StepResult : IReadModel
    {
        /// <summary>
        /// Gets or sets the <see cref="StepResultSeverity">severity</see> for the result
        /// </summary>
        public StepResultSeverity Severity { get; set; }

        /// <summary>
        /// Gets or sets the project the result belongs to
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Gets or sets the file the result is for
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the line the result is for
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Gets or sets the column the result is for
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the language specific compiler error/warning code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the msssage
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the original line in the log the result stems from
        /// </summary>
        public int OriginalLine { get; set; }
    }
}