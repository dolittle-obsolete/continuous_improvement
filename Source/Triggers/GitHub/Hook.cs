/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Triggers.GitHub
{
    #pragma warning disable 1591
    public class Hook
    {
        public string type;
        public int id;
        public string name;
        public bool active;
        public string[] events;
        public HookConfig config;

        public DateTimeOffset updated_at;
        public DateTimeOffset created_at;

        public string url;
        public string test_url;
        public string ping_url;

        public LastResponse last_response;
    }
}