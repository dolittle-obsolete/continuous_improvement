/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Concepts;
using Concepts.Improvables;

namespace Policies.Improvements
{
    public interface IImprovementContextFactory
    {
        ImprovementContext GetFor(ImprovableId improvable, VersionString version);
    }
}