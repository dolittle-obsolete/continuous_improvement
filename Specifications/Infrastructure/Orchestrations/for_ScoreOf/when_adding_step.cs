/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_ScoreOf
{
    public class when_adding_step
    {
        class score_context {}
        class performer : IPerformer<score_context>
        {
            public bool CanPerform(score_context score)
            {
                return true;
            }

            public Task Perform(score_context score)
            {
                return Task.CompletedTask;
            }
        }

        static ScoreOf<score_context>   score;

        Establish context = () => score = new ScoreOf<score_context>(new score_context());
        
        Because of = () => score.AddStep<performer>();

        It should_hold_the_type_in_steps = () => score.Steps.ShouldContainOnly(typeof(performer));       
    }
}