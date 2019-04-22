/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using Concepts.SourceControl;
using Dolittle.Machine.Specifications.Events;
using Domain.Improvables;
using Events.Improvables;
using Machine.Specifications;

namespace Domain.Specs.for_Improvable.when_registering.when_handling_the_register_command
{
    [Subject(typeof(CommandHandler))]
    public class for_an_existing_improvable : given.a_command_handler_for<for_an_existing_improvable>
    {
        static RegisterImprovable register_improvable;
        static string repository;
        static string name;
        static string recipe;
        static string path;
        static Exception exception;

        Establish context = () => 
        {
            repository = "the repository";
            name = "the name";
            path = "the path";
            recipe = "the recipe";
            register_improvable = new RegisterImprovable
            {
                Recipe = recipe,
                Repository = repository,
                Name = name,
                Path = path,
                Improvable = id.Value
            };
            command_handler.Handle(register_improvable);
            improvable.Commit();
        };

        Because of = () => exception = Catch.Exception(() => command_handler.Handle(register_improvable));

        It should_fail_with_an_already_registered_error = () => exception.ShouldBeOfExactType<ImprovableAlreadyRegistered>();
    }
}