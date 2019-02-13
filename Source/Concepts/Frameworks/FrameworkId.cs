using System;
using Dolittle.Concepts;

namespace Concepts.Frameworks
{
    /// <summary>
    /// Represents the unique identifier for a framework
    /// </summary>
    public class FrameworkId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="FrameworkId"/>
        /// </summary>
        /// <param name="value"><see cref="Guid"/> to convert from</param>
        public static implicit operator FrameworkId(Guid value)
        {
            return new FrameworkId { Value = value };
        }
    }
}