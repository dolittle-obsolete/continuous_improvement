/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Core.SourceControl.GitHub;
using Domain.SourceControl.GitHub;
using Machine.Specifications;
using Microsoft.AspNetCore.Http;

namespace Core.Specs.SourceControl.GitHub.for_InstallationCallbackHandler.when_installing
{
    public class and_the_installation_is_successful
        : given.an_installation_callback_handler_for<and_the_installation_is_successful>
    {
        static HttpResponse response;
        static long installation_id;
        static string successful_url; 

        Establish context = () => 
        {
            installation_id = 100;
            response = new given.fake_http_response();
            command_coordinator.Setup(_ => _.Handle(Moq.It.Is<RegisterInstallation>(c => c.Id == installation_id )))
                                    .Returns(new Dolittle.Runtime.Commands.CommandResult());
            command_coordinator.Setup(_ => _.Handle(Moq.It.Is<RegisterInstallation>(c => c.Id != installation_id )))
                                    .Returns(new Dolittle.Runtime.Commands.CommandResult(){ Exception = new Exception()});                        
            successful_url = $"{base_url}{InstallationCallbackHandler.SUCCESS}{installation_id}";
        };

        Because of = () => handler.Install(installation_id, response);

        It should_register_the_specified_installation = () => command_coordinator.Verify(_ => _.Handle(Moq.It.Is<RegisterInstallation>(c => c.Id == installation_id )),Moq.Times.Once());
        It should_indicate_that_the_response_had_been_redirected = () => response.StatusCode.ShouldEqual(StatusCodes.Status303SeeOther);
        It should_redirect_to_the_successful_created_repository_url = () => 
        {
            (response.Headers["Location"].First()).ShouldEqual(successful_url);
        };
    }
}
