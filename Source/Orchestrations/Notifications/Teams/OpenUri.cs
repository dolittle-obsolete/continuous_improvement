/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Orchestrations.Notifications.Teams
{
    #pragma warning disable 1591
    public class OpenUri : PotentialAction
    {
        public string @type = "OpenUri";
        public IEnumerable<Target> targets;
    }
}