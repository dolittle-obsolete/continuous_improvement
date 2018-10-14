/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Dolittle.Tenancy;
using Infrastructure.Orchestrations;
using Read.Configuration;

namespace Orchestrations
{
    /// <summary>
    /// Defines a system that can configure the <see cref="ScoreOf{Context}">score</see>
    /// </summary>
    public interface IScoreConfigurator
    {
        /// <summary>
        /// Configure a <see cref="ScoreOf{Context}">score</see>
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId">Tenant</see> to configure for</param>
        /// <param name="project"><see cref="Project"/> to configure for</param>
        /// <param name="commit">Commit identifier</param>
        /// <param name="isPullRequest">Whether or not this is a pull request</param>
        /// <returns>A new <see cref="ScoreOf{Context}">score</see></returns>
        ScoreOf<Context> From(TenantId tenantId, Project project, string commit, bool isPullRequest);
    }
}