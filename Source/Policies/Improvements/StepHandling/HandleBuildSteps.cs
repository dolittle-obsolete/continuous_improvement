using System.Collections.Generic;
using System.Linq;
using Dolittle.DependencyInversion;
using Policies.Improvements.Tracking;

namespace Policies.Improvements.StepHandling
{

    public class HandleBuildSteps : IHandleBuildSteps
    {
        FactoryFor<IImprovementStepResultHandler> _stepResultHandlerFactory;

        public HandleBuildSteps(FactoryFor<IImprovementStepResultHandler> stepResultHandlerFactory)
        {
            _stepResultHandlerFactory = stepResultHandlerFactory;
        }
        public void Handle(Domain.Improvements.Metadata.ImprovementMetadata metadata, IEnumerable<TrackedStepStatuses> steps)
        {
            var unhandledSteps = steps.Where(_ => !_.HasBeenHandled).ToList();
            var unhandledFailedSteps = unhandledSteps.Where(_ => _.HasFailed).ToList();
            var unhandledSucceededSteps = unhandledSteps.Where(_ => _.HasSucceeded).ToList();

            var handler = _stepResultHandlerFactory();
            unhandledFailedSteps.ForEach(_ => handler.HandleFailedStep(metadata.Recipe,_.Step,metadata.Improvement,metadata.ImprovementFor,metadata.Version));
            unhandledSucceededSteps.ForEach(_ => handler.HandleSuccessfulStep(metadata.Recipe,_.Step,metadata.Improvement,metadata.ImprovementFor,metadata.Version));
        }
    }
}