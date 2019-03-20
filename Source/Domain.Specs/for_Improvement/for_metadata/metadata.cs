using System;
using System.Collections.Generic;
using Concepts.Improvables;
using Dolittle.Collections;
using Domain.Improvements.Metadata;

namespace Domain.Specs.for_Improvement.for_metadata
{
    public class metadata
    {
        public static readonly string valid_tenant_id = Guid.NewGuid().ToString();
        public static readonly string valid_improvement_for = Guid.NewGuid().ToString();
        public static readonly string valid_improvement_id = Guid.NewGuid().ToString();
        public const string valid_recipe_type = "my_type";
        public const string valid_version = "1.1.1";

        public static IDictionary<string,string> get_valid()
        {
            return new Dictionary<string,string>
            {
                { Constants.Tenant, valid_tenant_id },
                { Constants.Improvable, valid_improvement_for },
                { Constants.Improvement, valid_improvement_id },
                { Constants.RecipeType, valid_recipe_type },
                { Constants.Version, valid_version }
            };
        }
    }
}