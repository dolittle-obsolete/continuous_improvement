/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    public class ImprovementRequested : IEvent
    {
        public ImprovementRequested(Guid improvable, string version, bool pullRequest) 
        {
            Improvable = improvable;
            Version = version;
            PullRequest = pullRequest;
        }

        public Guid Improvable { get; }

        public string Version {  get; }

        public bool PullRequest { get; }
    }
}