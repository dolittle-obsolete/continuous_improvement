using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Commands;

namespace Domain.Improvements
{
    public class InitiateImprovement : ICommand
    {
        public ImprovementId Improvement { get; set; }
        public ImprovableId ForImprovable { get; set; }
        public Version Version { get; set; }
        public bool IsFromPullRequest { get; set; }
    }
}
