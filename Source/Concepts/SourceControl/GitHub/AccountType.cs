using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    public class AccountType : ConceptAs<string>
    {
        public static implicit operator AccountType(string value)
        {
            return new AccountType {Value = value};
        }
    }
}
