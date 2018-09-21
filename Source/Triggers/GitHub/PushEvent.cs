/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Triggers.GitHub
{
    #pragma warning disable 1591
    public class PushEvent
    {
        public string @ref; 
        public string before;
        public string after;
        public bool created;
        public bool deleted;
        public bool forced;
        public string base_ref;
        public string compare;
        public Commit[] commits;
        public Commit head_commit;
        public Repository repository;
        public Pusher pusher;
        public Organization organization;
        public User sender;
    }
}