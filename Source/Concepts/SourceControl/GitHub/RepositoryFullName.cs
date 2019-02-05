using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    public class RepositoryFullName : ConceptAs<string>
    {
        public static implicit operator RepositoryFullName(string value)
        {
            return new RepositoryFullName {Value = value};
        }
    }
}
