/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Globalization;
using System.Security.Claims;
using Dolittle.DependencyInversion;
using Dolittle.ReadModels;
using Dolittle.ReadModels.MongoDB;
using Dolittle.Runtime.Events.Store;
using Dolittle.Runtime.Events.Store.MongoDB;
using Dolittle.Security;
using MongoDB.Driver;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public class NullBindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO");
            var client = new MongoClient(mongoConnectionString);
 	        var database = client.GetDatabase("CI_EventStore");
            var eventStoreConfig = new EventStoreConfig(database,Dolittle.Logging.Logger.Internal);
            builder.Bind<EventStoreConfig>().To(eventStoreConfig);
 	 
 	        builder.Bind<IEventStore>().To<Dolittle.Runtime.Events.Store.MongoDB.EventStore>();            

            builder.Bind<Dolittle.ReadModels.MongoDB.Configuration>().To(new Dolittle.ReadModels.MongoDB.Configuration
            {
                Url = mongoConnectionString,
                UseSSL = false,
                DefaultDatabase = "CI_Read"
            });
            builder.Bind(typeof(IReadModelRepositoryFor<>)).To(typeof(ReadModelRepositoryFor<>));
        }
    }
}