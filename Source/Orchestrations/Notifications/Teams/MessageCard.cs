/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Orchestrations.Notifications.Teams
{
    #pragma warning disable 1591
    public class MessageCard
    {
        public string @context => "http://scheme.org/extensions";
        public string @type => "MessageCard";
        public string themeColor;
        public string title;
        public string summary;
        public string text;
        public IEnumerable<PotentialAction> potentialAction;
        public IEnumerable<Section> sections;
    }
}