/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.DependencyInversion;
using Infrastructure.Services.Github.Installation;

namespace Core.SourceControl.GitHub
{
    /// <summary>
    /// Creates the IoC bindings necessary for SourceControl.GitHub
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc />
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanHandleInstallationCallbacks>().To<InstallationCallbackHandler>();
        }
    }
}