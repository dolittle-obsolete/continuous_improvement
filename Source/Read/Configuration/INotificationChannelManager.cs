/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Concepts.Configuration;

namespace Read.Configuration
{
    public interface INotificationChannelManager
    {
        IEnumerable<NotificationChannel> GetAll();
        NotificationChannel GetById(NotificationChannelId notificationChannel);
    }

}