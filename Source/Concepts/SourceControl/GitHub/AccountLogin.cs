using Dolittle.Concepts;

namespace Concepts.SourceControl.GitHub
{
    public class AccountLogin : ConceptAs<string>
    {
        public static implicit operator AccountLogin(string value)
        {
            return new AccountLogin {Value = value};
        }
    }
}
