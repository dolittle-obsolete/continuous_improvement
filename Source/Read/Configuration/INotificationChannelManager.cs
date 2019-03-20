/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;

namespace Read.Configuration
{
    /// <summary>
    /// Defines a manager for notification channels
    /// </summary>
    public interface INotificationChannelManager
    {
        /// <summary>
        /// Gets all <see cref="NotificationChannel">notification channels</see>
        /// </summary>
        IEnumerable<NotificationChannel> GetAll();

        /// <summary>
        /// Gets the <see cref="NotificationChannel" /> with the specified Id
        /// </summary>
        /// <param name="notificationChannel">The notification channel id</param>
        /// <returns></returns>
        NotificationChannel GetById(NotificationChannelId notificationChannel);
    }

}