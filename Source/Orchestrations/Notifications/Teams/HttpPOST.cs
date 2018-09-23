/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Orchestrations.Notifications.Teams
{
    #pragma warning disable 1591
    public class HttpPOST : Action
    {
        public string @type = "HttpPOST";
        public string target;       
    }
}