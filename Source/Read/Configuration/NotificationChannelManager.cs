/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Concepts.Configuration;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Serialization.Json;

namespace Read.Configuration
{
    /// <inheritdoc />
    public class NotificationChannelManager : INotificationChannelManager
    {
        const string _notificationChannelsFils = "notificationChannels.json";
        
        private readonly ISerializer _serializer;
        private readonly IFiles _fileSystem;

        /// <summary>
        /// Instantiates an instance of <see cref="NotificationChannelManager" />
        /// </summary>
        /// <param name="fileSystem">A file system wrapper</param>
        /// <param name="serializer">A serializer</param>
        public NotificationChannelManager(IFiles fileSystem, ISerializer serializer)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;
        }

        /// <inheritdoc />
        public IEnumerable<NotificationChannel> GetAll()
        {
            if( !_fileSystem.Exists(_notificationChannelsFils)) return new NotificationChannel[0];
            var json = _fileSystem.ReadAllText(_notificationChannelsFils);
            var notificationChannels = _serializer.FromJson<IEnumerable<NotificationChannel>>(json);
            return notificationChannels;
        }
        
        /// <inheritdoc />
        public NotificationChannel GetById(NotificationChannelId notificationChannelId)
        {
            var notificationChannel = GetAll().Single(_ => _.Id == notificationChannelId);
            return notificationChannel;
        }
    }
}