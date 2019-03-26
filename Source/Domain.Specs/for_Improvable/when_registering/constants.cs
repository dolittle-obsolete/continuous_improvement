using System;
using System.Collections.Generic;
using Concepts.Improvables;
using Concepts.SourceControl;
using Dolittle.Collections;
using Domain.Improvables;

namespace Domain.Specs.for_Improvable.when_registering
{
    public class constants
    {
        public readonly static ImprovableId valid_improvable_id = Guid.NewGuid();
        public readonly static ImprovableName valid_improvable_name = "MyImprovable";
        public readonly static RecipeType valid_recipe_type = "AllTheThings!";
        public readonly static RepositoryFullName valid_repository = "Some Repo on Github";
        public readonly static Path valid_path = "a valid path within the repo";
        public readonly static ImprovableId improvable_exists = Guid.NewGuid();
        public readonly static ImprovableName improvable_name_exists = "improvable with this name already exists";

        public static RegisterImprovable get_command(IEnumerable<Action<RegisterImprovable>> invalidations = null)
        {
            var improvable = get_valid_command();
            if(invalidations != null)
                invalidations.ForEach(_ => _(improvable));
            return improvable;
        }

        public static RegisterImprovable get_command(Action<RegisterImprovable> invalidation)
        {
            var improvable =  get_valid_command();
            invalidation(improvable);
            return improvable;
        }

        static RegisterImprovable get_valid_command()
        {
            return new RegisterImprovable
            {
                Improvable = constants.valid_improvable_id,
                Name = constants.valid_improvable_name,
                Recipe = constants.valid_recipe_type,
                Repository = constants.valid_repository,
                Path = constants.valid_path
            };
        }
    }
}