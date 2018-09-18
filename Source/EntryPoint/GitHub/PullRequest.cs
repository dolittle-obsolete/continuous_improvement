/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace EntryPoint.GitHub
{
    #pragma warning disable 1591
    public class PullRequest
    {
        public string url;
        public string id;
        public string node_id;
        public string html_url;
        public string diff_url;
        public string patch_url;
        public string issue_url;
        public int number;
        public string state;
        public bool locked;
        public string title;
        public User user;
        public string body;
        public DateTimeOffset created_at;
        public DateTimeOffset updated_at;
        public DateTimeOffset closed_at;
        public DateTimeOffset merged_at;
        public string merge_commit_sha;
        public User assignee;
        public User[] assignees;
        public User[] requested_reviewers;
        public Label[] labels;

        public PullRequestCommit head;
        public PullRequestCommit @base;
    }
}