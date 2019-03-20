using System;
using System.Collections.Generic;
using Concepts.Improvables;
using Dolittle.Collections;
using Domain.Improvements.Metadata;

namespace Domain.Specs.for_Improvement.for_metadata
{

    public static class metadata_extensions
    {
        public static void invalidate(this IDictionary<string,string> existing, params string[] propertiesToInvalidate)
        {
            propertiesToInvalidate.ForEach(p => existing[p] = string.Empty);
        }

        public static void remove(this IDictionary<string,string> existing, params string[] propertiesToRemove)
        {
            propertiesToRemove.ForEach(p => existing.Remove(p));
        }
    }
}