/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Text.RegularExpressions;
using Dolittle.Concepts;

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents a semantic <see cref="StepId" />
    /// </summary>
    public class StepId : Value<StepId>
    {
        static readonly Regex _regex = new Regex(@"^step-(\d+)-(\d+)-?(.*)$", RegexOptions.Compiled);

        /// <summary>
        ///The state of a <see cref="StepId" /> that has not been set.
        /// </summary>
        public static StepId Empty { get; } = string.Empty;

        /// <summary>
        /// Instantiate a <see cref="StepId" /> with the Empty state
        /// </summary>
        /// <returns></returns>
        public StepId() : this(string.Empty)
        {}

        /// <summary>
        /// Instantiate a <see cref="StepId" /> from the string representation
        /// </summary>
        /// <param name="value"></param>
        public StepId(string value)
        {
            var match = _regex.Match(value);
            if(match.Success)
            {
                StepNumber = int.Parse(match.Groups[1].Value);
                SubStepNumber = int.Parse(match.Groups[2].Value);
                Container = match.Groups[3].Value;
            }
            else
            {
                StepNumber = StepNumber.Empty;
                SubStepNumber = StepNumber.Empty;
                Container = string.Empty;
            }
        }

        /// <summary>
        /// Instantiate a <see cref="StepId" /> from the component parts
        /// </summary>
        /// <param name="stepNumber">The number of the step</param>
        /// <param name="subStepNumber">The number of the substep</param>
        /// <param name="container">The name of the container</param>
        public StepId(int stepNumber, int subStepNumber, string container)
        {
            StepNumber = stepNumber;
            SubStepNumber = subStepNumber;
            Container = container;
        }        

        /// <summary>
        /// Implicitly convert a string to a <see cref="StepId" />
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator StepId(string value) => new StepId(value);

        /// <summary>
        /// Gets the <see cref="StepNumber" /> from this Id
        /// </summary>
        public StepNumber StepNumber { get; }
        /// <summary>
        /// Gets the <see cref="StepNumber">Substep Number</see>from this Id
        /// </summary>
        public StepNumber SubStepNumber { get; }
        /// <summary>
        /// Gets the Container from this Id
        /// </summary>
        public string Container { get; }

        /// <summary>
        /// Generates a string representation of this Id
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"step-{StepNumber}-{SubStepNumber}-{Container??string.Empty}";
        }

        /// <summary>
        /// Indicates whether this is a valid Id.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return StepNumber != StepNumber.Empty;
        }
    }
}
