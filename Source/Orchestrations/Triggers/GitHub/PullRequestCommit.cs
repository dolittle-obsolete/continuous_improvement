/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Orchestrations.Triggers.GitHub
{
    #pragma warning disable 1591
    public class PullRequestCommit
    {
        public string label;
        public string @ref;
        public string sha;
        public User user;
        public Repository repository;
    }
}