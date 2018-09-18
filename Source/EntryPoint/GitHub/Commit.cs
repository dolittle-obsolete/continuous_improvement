/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace EntryPoint.GitHub
{
    #pragma warning disable 1591
    public class Commit
    {
        public string id;
        public string tree_id;
        public bool distinct;
        public string message;
        public DateTimeOffset timestamp;
        public string url;
        public Author author;
        public Author committer;
        public string[] added;
        public string[] removed;
        public string[] modified;
    }
}