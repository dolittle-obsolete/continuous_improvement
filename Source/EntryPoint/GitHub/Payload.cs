/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace EntryPoint.GitHub
{
#pragma warning disable 1591
    public class Payload
    {
        public string zen;
        public int number;
        public PullRequest pull_request;
        public string @ref; 
        public string before;
        public string after;
        public bool created;
        public bool deleted;
        public bool forced;
        public string compare;
        public Commit[] commits;
        public Commit head_commit;
        public Repository repository;
        public Pusher pusher;
        public int hook_id;
        public Hook hook;  
        public Organization organization;     
        public User sender;       
    }
}