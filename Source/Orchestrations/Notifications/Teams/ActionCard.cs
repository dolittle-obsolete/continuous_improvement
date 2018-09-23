/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Orchestrations.Notifications.Teams
{
    #pragma warning disable 1591
    public class ActionCard : PotentialAction
    {
        public string @type => "ActionCard";
        public IEnumerable<Input> inputs;
        public IEnumerable<Action> actions;
    }
}