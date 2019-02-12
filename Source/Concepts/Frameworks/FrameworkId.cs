using Dolittle.Concepts;
using System;

namespace Concepts.Frameworks
{
    public class FrameworkId : ConceptAs<Guid>
    {
        public static implicit operator FrameworkId(Guid value)
        {
            return new FrameworkId {Value = value};
        }
    }
}
