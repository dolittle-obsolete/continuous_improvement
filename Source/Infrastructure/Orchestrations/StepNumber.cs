/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Infrastructure.Orchestrations
{  
    /// <summary>
    /// Represents the number of a <see cref="Step"/>
    /// </summary>
    public class StepNumber : ConceptAs<int>
    {
        /// <summary>
        /// Implicitly convert from <see cref="int"/> to <see cref="StepNumber"/>
        /// </summary>
        /// <param name="number">Integer to convert from</param>
        public static implicit operator StepNumber(int number)
        {
            return new StepNumber() { Value = number };
        }
    }
}