using System;
using Concepts.Improvables;
using Concepts.SourceControl;

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
        public readonly static RepositoryFullName repository_name_exists = "repo with this name already exists";
        public readonly static RecipeType recipe_exists = "recipe exists";
    }
}