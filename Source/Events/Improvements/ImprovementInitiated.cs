/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Events;

namespace Events.Improvements
{
    public class ImprovementInitiated : IEvent
    {
        public ImprovementInitiated(Guid forImprovable, string version, bool pullRequest) 
        {
            ForImprovable = forImprovable;
            Version = version;
            PullRequest = pullRequest;
        }

        public Guid ForImprovable { get; }

        public string Version {  get; }

        public bool PullRequest { get; }
    }
}