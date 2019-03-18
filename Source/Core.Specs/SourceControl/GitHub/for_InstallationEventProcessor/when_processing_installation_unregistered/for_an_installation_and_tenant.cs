/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Core.SourceControl.GitHub;
using Events.SourceControl.GitHub;
using Machine.Specifications;

namespace Core.Specs.SourceControl.GitHub.for_InstallationEventProcessor.when_processing_installation_unregistered
{
    [Subject(typeof(InstallationEventProcessor), "Process(InstallationUnregistered)")]
    public class for_an_installation_and_tenant
        : given.an_installation_event_processor_for<for_an_installation_and_tenant>
        {
            protected static InstallationUnregistered unregistered_event;

            Establish context = () =>
            {
                unregistered_event = new InstallationUnregistered(installation_id, null);
            };

            Because of = () => event_processor.Process(unregistered_event);

            It should_disassociate_the_installation_from_the_current_tenant = () =>
            installation_to_tenant_mapper.Verify(_ =>
                _.DisassociateTenantFromInstallation(installation_id),
                Moq.Times.Once()
            );
        }
}