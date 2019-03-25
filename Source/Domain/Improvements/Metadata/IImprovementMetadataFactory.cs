/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Domain.Improvements.Metadata
{
    /// <summary>
    /// Defines a factory for build the <see cref="ImprovementMetadata" />
    /// </summary>
    public interface IImprovementMetadataFactory
    {
        /// <summary>
        /// Builds a strongly types <see cref="ImprovementMetadata" /> from the raw data
        /// </summary>
        /// <param name="source">Key value pairs of metadata</param>
        /// <param name="sourceId">The id of the source</param>
        /// <returns>The <see cref="ImprovementMetadata" /> </returns>
        ImprovementMetadata BuildFrom(IDictionary<string,string> source, string sourceId);
    }
}