using Concepts;
using Concepts.Improvements;
using Concepts.Improvables;
using Dolittle.Commands.Validation;
using FluentValidation;
namespace Domain.Improvements
{
    public class InitiateImprovementInputValidator : CommandInputValidatorFor<InitiateImprovement>
    {
        public InitiateImprovementInputValidator()
        {
            RuleFor(_ => _.Improvement)
                .MustBeAValidImprovementId();
            RuleFor(_ => _.ForImprovable)
                .MustBeAValidImprovableId();
            RuleFor(_ => _.Version)
                .MustBeAValidVersion();
        }
    }
}
