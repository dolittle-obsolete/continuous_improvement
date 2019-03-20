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

namespace Core.Specs.SourceControl.GitHub.for_InstallationCallbackHandler.when_updating
{
    public class in_all_cases
        : given.an_installation_callback_handler_for<in_all_cases>
    {
        static HttpResponse response;
        static long installation_id;
        static string update_url; 

        Establish context = () => 
        {
            installation_id = 100;
            response = new given.fake_http_response();                        
            update_url = $"{base_url}{InstallationCallbackHandler.UPDATED}{installation_id}";
        };

        Because of = () => handler.Update(installation_id, response);

        It should_not_issue_any_commands = () => command_coordinator.Verify(_ => _.Handle(Moq.It.IsAny<RegisterInstallation>()),Moq.Times.Never());
        It should_indicate_that_the_response_had_been_redirected = () => response.StatusCode.ShouldEqual(StatusCodes.Status303SeeOther);
        It should_redirect_to_the_updated_repository_url = () => 
        {
            (response.Headers["Location"].First()).ShouldEqual(update_url);
        };
    }
}
