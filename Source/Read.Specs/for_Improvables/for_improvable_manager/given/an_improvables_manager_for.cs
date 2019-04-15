/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Read.Improvables;
using Concepts.Improvables;
using Dolittle.IO;
using Dolittle.IO.Tenants;
using Dolittle.Serialization.Json;
using Moq;
using System.Collections.Generic;
using System;
using System.IO;

namespace Read.Specs.for_Improvables.for_improvable_manager.given
{
    public class an_improvables_manager_for<T>
    {
        protected const string improvable_name_that_exists = "exists";
        protected const string improvable_name_that_does_not_exist = "does not exist";
        protected const string text_from_improvables_file = "text from improvables file";
        protected const string text_from_improvable_file = "text from improvable file";
        protected static string path_to_all_improvables;
        protected static string path_to_existing_improvable_file;
        protected static string path_to_non_existing_improvable_file;
        protected static string path_to_unreadable_improvable_file;
        protected static List<ImprovableForListing> improvables_for_listing;
        protected static Improvable improvable;
        protected static ImprovableId improvable_that_exists;
        protected static ImprovableId improvable_that_is_unreadable;
        protected static ImprovableId improvable_that_does_not_exist;
        protected static IImprovableManager improvable_manager;
        protected static Mock<IFiles> file_system;
        protected static Mock<ISerializer> serializer;
        protected static Mock<IRecipeManager> recipe_manager;
        protected static bool improvables_file_exists = true;

        Establish context = () => 
        {
            improvable_that_exists = Guid.NewGuid();
            improvable_that_does_not_exist = Guid.NewGuid();
            improvable_that_is_unreadable = Guid.NewGuid();
            file_system = new Mock<IFiles>();
            serializer = new Mock<ISerializer>();
            recipe_manager = new Mock<IRecipeManager>();

            improvable_manager = new ImprovableManager(file_system.Object, serializer.Object, recipe_manager.Object);
            improvables_for_listing = new List<ImprovableForListing>();
            improvables_for_listing.Add(new ImprovableForListing() { Id = improvable_that_exists, Name = improvable_name_that_exists, Version = "version", Status = ImprovableStatus.InProgress });
            improvables_for_listing.Add(new ImprovableForListing() { Id = Guid.NewGuid(), Name = "Second", Version = "version", Status = ImprovableStatus.InProgress });

            path_to_existing_improvable_file = Path.Combine(improvable_that_exists.ToString(), ImprovableManager.IMPROVABLE);
            path_to_non_existing_improvable_file = Path.Combine(improvable_name_that_does_not_exist.ToString(), ImprovableManager.IMPROVABLE);
            path_to_unreadable_improvable_file = Path.Combine(improvable_that_is_unreadable.ToString(), ImprovableManager.IMPROVABLE);

            file_system.Setup(_ => _.Exists(Moq.It.Is<string>(s => s == ImprovableManager.IMPROVABLES))).Returns(improvables_file_exists);
            file_system.Setup(_ => _.Exists(Moq.It.IsAny<string>())).Returns((string s) => s == path_to_existing_improvable_file || s == path_to_unreadable_improvable_file);
            file_system.Setup(_ => _.ReadAllText(Moq.It.Is<string>(s => s == path_to_existing_improvable_file))).Returns(text_from_improvable_file);
            file_system.Setup(_ => _.ReadAllText(Moq.It.Is<string>(s => s == path_to_unreadable_improvable_file))).Throws(new Exception());
            
            improvable = new Improvable() { Id = improvable_that_exists, Name = improvable_name_that_exists };
            serializer.Setup(_ => _.FromJson<IEnumerable<ImprovableForListing>>(text_from_improvables_file,Moq.It.IsAny<ISerializationOptions>())).Returns(improvables_for_listing);
            serializer.Setup(_ => _.FromJson<Improvable>(Moq.It.IsAny<string>(),Moq.It.IsAny<ISerializationOptions>())).Returns(improvable);     
        };
    }
}