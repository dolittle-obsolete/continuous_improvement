/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Machine.Specifications;

namespace Infrastructure.Orchestrations.for_Conductor
{
    public class when_conducting_a_score_with_step_that_needs_configuration : given.a_conductor
    {
        class configuration {}

        class performer : IPerformer<score_context>, INeedConfigurationOf<configuration>
        {
            public configuration Config { get; set; }
            public IPerformerLog Log;

            public bool CanPerform(score_context score)
            {
                return true;
            }

            public Task Perform(IPerformerLog log, score_context score)
            {
                Log = log;
                return Task.CompletedTask;
            }
        }

        static configuration configuration_instance;

        static performer performer_instance;
        static ScoreOf<score_context> score;

        Establish context = () => 
        {
            performer_instance = new performer();
            configuration_instance = new configuration();

            container.Setup(_ => _.Get(typeof(performer))).Returns(performer_instance);
            score = new ScoreOf<score_context>(new score_context());
            score.AddStep<performer,configuration>(configuration_instance);
        };

        Because of = () => conductor.Conduct(score);

        It should_pass_a_performer_log = () => performer_instance.Log.ShouldNotBeNull();
        It should_pass_configuration_to_performer = () => performer_instance.Config.ShouldEqual(configuration_instance);
    }
}