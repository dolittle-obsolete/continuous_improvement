/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Orchestrations.SourceControl
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that will deal with getting the latest source code
    /// </summary>
    public class GetLatest : IPerformer<Context>
    {
        /// <inheritdoc/>
        public Task Perform(Context score)
        {
            var gitFolder = Path.Combine(score.Source, ".git");
            if (!Directory.Exists(gitFolder))
            {
                var cloneOptions = new CloneOptions();
                cloneOptions.RecurseSubmodules = true;

                /*
                cloneOptions.CredentialsProvider = (_url, _user, _cred) => 
                                                        new UsernamePasswordCredentials 
                                                        {
                                                            Username = "username", 
                                                            Password = "password"
                                                        };
                */

                Repository.Clone(score.Project.Repository.ToString(), score.Source, cloneOptions);
            }
            else
            {
                using(var repo = new Repository(score.Source))
                {
                    var pullOptions = new PullOptions();
                    pullOptions.FetchOptions = new FetchOptions();
                    var signature = new Signature(
                        new Identity("<Dolittle CI>", "post@dolittle.com"), DateTimeOffset.Now);

                    Commands.Pull(repo, signature, pullOptions);
                }
            }

            // Checkout specific commit

            return Task.CompletedTask;
        }
    }
}