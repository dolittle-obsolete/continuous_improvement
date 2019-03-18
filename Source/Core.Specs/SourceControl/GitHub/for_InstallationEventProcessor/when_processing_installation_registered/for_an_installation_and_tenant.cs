/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Core.SourceControl.GitHub;
using Events.SourceControl.GitHub;
using Machine.Specifications;

namespace Core.Specs.SourceControl.GitHub.for_InstallationEventProcessor.when_processing_installation_registered
{
    [Subject(typeof(InstallationEventProcessor), "Process(InstallationRegistered)")]
    public class for_an_installation_and_tenant
        : given.an_installation_event_processor_for<for_an_installation_and_tenant>
        {
            protected static InstallationRegistered registered_event;

            Establish context = () =>
            {
                registered_event = new InstallationRegistered(installation_id, null, null, null);
            };

            Because of = () => event_processor.Process(registered_event);

            It should_associate_the_installation_with_the_current_tenant = () =>
            installation_to_tenant_mapper.Verify(_ =>
                _.AssociateTenantWithInstallation(installation_id, tenant),
                Moq.Times.Once()
            );
        }
}