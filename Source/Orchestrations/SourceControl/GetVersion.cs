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
        public Task Perform(Context score)
        {
            using(var repo = new Repository(score.Source))
            { 
                var tag = repo.Tags.ToArray().LastOrDefault();
                if( tag != null ) score.Version = $"{tag.FriendlyName}.{score.BuildNumber}";
            }

            return Task.CompletedTask;
        }
    }
}