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
        /// Implicitly convert from <see cref="int"/> to <see cref="StepNumber"/>
        /// </summary>
        /// <param name="value">Integer to convert from</param>
        public static implicit operator StepNumber(int value)
        {
            return new StepNumber {Value = value};
        }
    }
}
