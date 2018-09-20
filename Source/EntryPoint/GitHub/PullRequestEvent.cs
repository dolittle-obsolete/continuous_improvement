/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace EntryPoint.GitHub
{
#pragma warning disable 1591
    public class PullRequestEvent
    {
        public string action;
        public int number;
        public PullRequest pull_request;
        public Repository repository;
        public Organization organization;
        public User sender;
    }
}