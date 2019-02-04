using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    public class InstallationId : ConceptAs<long>
    {
        public static implicit operator InstallationId(long value)
        {
            return new InstallationId {Value = value};
        }
    }
}
