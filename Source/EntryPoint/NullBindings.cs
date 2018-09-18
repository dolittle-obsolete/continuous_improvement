/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Runtime.Execution;
using Dolittle.Security;

namespace EntryPoint
{
    /// <summary>
    /// 
    /// </summary>
    public class NullBindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ExecutionContextPopulator>().To((ExecutionContext, details) => { });
            builder.Bind<ClaimsPrincipal>().To(() => new ClaimsPrincipal(new ClaimsIdentity()));
            builder.Bind<CultureInfo>().To(() => CultureInfo.InvariantCulture);
            builder.Bind<ICallContext>().To(new DefaultCallContext());
            builder.Bind<ICanResolvePrincipal>().To(new DefaultPrincipalResolver());
        }
    }
}