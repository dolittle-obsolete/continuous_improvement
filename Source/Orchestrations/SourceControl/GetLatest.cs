/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Orchestrations;
using LibGit2Sharp;

namespace Orchestrations.SourceControl
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that will deal with getting the latest source code
    /// </summary>
    public class GetLatest : IPerformer<Context>
    {
        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {
            return !score.IsPullRequest;
        }

        /// <inheritdoc/>
        public Task Perform(IPerformerLog log, Context score)
        {
            log.Information($"Get latest source into '{score.SourcePath}'");

            var gitFolder = Path.Combine(score.SourcePath, ".git");
            if (!Directory.Exists(gitFolder))
                Clone(log, score);
            else
            {
                try
                {
                    Pull(log, score);
                } 
                catch
                {
                    log.Information("Problems pulling - recreating");
                    Directory.Delete(score.SourcePath, true);
                    Clone(log, score);
                }
            }

            return Task.CompletedTask;
        }

        void Pull(IPerformerLog log, Context score)
        {
            log.Information("Repository already exists - pulling latest");
            using(var repo = new Repository(score.SourcePath))
            {
                var pullOptions = new PullOptions();
                pullOptions.FetchOptions = new FetchOptions();
                var signature = new Signature(
                    new Identity("<Dolittle CI>", "post@dolittle.com"), DateTimeOffset.Now);

                pullOptions.FetchOptions.Prune = true;
                pullOptions.FetchOptions.TagFetchMode = TagFetchMode.All;
                pullOptions.FetchOptions.OnProgress = (string message) =>
                {
                    log.Information(message);
                    return true;
                };

                Commands.Pull(repo, signature, pullOptions);
            }
        }

        void Clone(IPerformerLog log, Context score)
        {
            log.Information("Cloning");
            var cloneOptions = new CloneOptions();
            cloneOptions.RecurseSubmodules = true;
            cloneOptions.OnCheckoutProgress = (string path, int completedSteps, int totalSteps) => log.Information(path);

            /*
            cloneOptions.CredentialsProvider = (_url, _user, _cred) => 
                                                    new UsernamePasswordCredentials 
                                                    {
                                                        Username = "username", 
                                                        Password = "password"
                                                    };
            */
            //if( Directory.Exists(score.SourcePath) ) Directory.Delete(score.SourcePath);

            Repository.Clone(score.Project.Repository.ToString(), score.SourcePath, cloneOptions);
        }
    }
}