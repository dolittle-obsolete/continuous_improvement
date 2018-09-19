/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Orchestrations.SourceControl
{
    /// <summary>
    /// Represents a <see cref="IPerformer{T}"/> that will deal with getting the version being built
    /// </summary>
    public class GetVersion : IPerformer<Context>
    {
        /// <inheritdoc/>
        public bool CanPerform(Context score)
        {
            return true;
        }

        /// <inheritdoc/>
        public Task Perform(Context score)
        {
            score.LogInformation("Getting version");
            using(var repo = new Repository(score.FullSourcePath))
            { 
                var tag = repo.Tags.ToArray().LastOrDefault();
                if( tag != null ) score.Version = $"{tag.FriendlyName}.{score.BuildNumber}";
                score.LogInformation($"Version is {score.Version}");
            }

            return Task.CompletedTask;
        }
    }
}