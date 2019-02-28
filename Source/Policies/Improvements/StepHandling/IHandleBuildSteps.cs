using System.Collections.Generic;
using Policies.Improvements.Tracking;

namespace Policies.Improvements.StepHandling
{
    public interface IHandleBuildSteps
    {
        void Handle(Domain.Improvements.Metadata.ImprovementMetadata improvementMetadata, IEnumerable<TrackedStepStatuses> steps);
    }
}