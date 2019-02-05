using Dolittle.Commands.Validation;

namespace Domain.SourceControl.GitHub
{
    public class RegisterInstallationBusinessValidator : CommandBusinessValidatorFor<RegisterInstallation>
    {
        // This command is triggered by an external event, so there is no point in trying to validate it
    }
}
