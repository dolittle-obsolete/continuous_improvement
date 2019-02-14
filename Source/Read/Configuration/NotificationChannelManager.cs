/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Configuration;
using Dolittle.IO.Tenants;
using Dolittle.Serialization.Json;

namespace Read.Configuration
{
    public class NotificationChannelManager : INotificationChannelManager
    {
        const string _notificationChannelsFils = "notificationChannels.json";
        
        private readonly ISerializer _serializer;
        private readonly ITenantAwareFileSystem _fileSystem;

        public NotificationChannelManager(ITenantAwareFileSystem fileSystem, ISerializer serializer)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;
        }

        public IEnumerable<NotificationChannel> GetAll()
        {
            if( !_fileSystem.Exists(_notificationChannelsFils)) return new NotificationChannel[0];
            var json = _fileSystem.ReadAllText(_notificationChannelsFils);
            var notificationChannels = _serializer.FromJson<IEnumerable<NotificationChannel>>(json);
            return notificationChannels;
        }
        
        public NotificationChannel GetById(NotificationChannelId notificationChannelId)
        {
            var notificationChannel = GetAll().Single(_ => _.Id == notificationChannelId);
            return notificationChannel;
        }
    }
}