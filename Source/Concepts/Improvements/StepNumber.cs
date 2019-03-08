/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Concepts.Improvements
{
    /// <summary>
    /// Represents the concept for a numbered step - the actual number
    /// </summary>
    public class StepNumber : ConceptAs<int>
    {
        /// <summary>
        /// Represent an Empty / Null / Not Set StepNumber
        /// </summary>
        public static StepNumber Empty { get; } = int.MinValue;

        /// <summary>
        /// Initializes a new instance of <see cref="StepNumber"/>
        /// </summary>
        public StepNumber() { }

        /// <summary>
        /// Instantiates an instance of an <see cref="StepNumber" /> with the specified value
        /// </summary>
        /// <param name="value"></param>
        public StepNumber(int value) => Value = value;

        /// <summary>
        /// Implicitly convert from <see cref="int"/> to <see cref="StepNumber"/>
        /// </summary>
        /// <param name="value">Integer to convert from</param>
        public static implicit operator StepNumber(int value) => new StepNumber(value);
    }
}