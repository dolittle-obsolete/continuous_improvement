/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Read.Configuration
{
    /// <summary>
    /// Represents the configuration of a notification
    /// </summary>
    public class NotificationChannel
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="NotificationChannel"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="NotificationChannel"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the configuration for the specific <see cref="NotificationChannel">notification channel type</see>
        /// </summary>
        /// <value></value>
        public object Configuration { get; set; }
    }
}