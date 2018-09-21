/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using Dolittle.DependencyInversion;
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
            builder.Bind<ClaimsPrincipal>().To(() => new ClaimsPrincipal(new ClaimsIdentity()));
            builder.Bind<CultureInfo>().To(() => CultureInfo.InvariantCulture);
            builder.Bind<ICanResolvePrincipal>().To(new DefaultPrincipalResolver());
        }
    }
}