/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Infrastructure.Orchestrations
{
    /// <summary>
    /// Defines a way for <see cref="IPerformer{T}"/> to get configuration
    /// </summary>
    public interface INeedConfigurationOf<T>
    {
        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        T Config { get; set; }
    }
}