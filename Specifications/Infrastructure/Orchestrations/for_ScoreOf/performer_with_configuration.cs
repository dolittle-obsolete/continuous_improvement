/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class performer_with_configuration : IPerformer<score_context>, INeedConfigurationOf<configuration>
    {
        public configuration Config { get; set; }

        public bool CanPerform(score_context score)
        {
            return true;
        }

        public Task Perform(score_context score)
        {
            return Task.CompletedTask;
        }
    }
}