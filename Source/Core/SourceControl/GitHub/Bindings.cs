using Dolittle.DependencyInversion;
using Infrastructure.Services.Github.Installation;

namespace Core.SourceControl.GitHub
{
    public class Bindings : ICanProvideBindings
    {
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanHandleInstallationCallbacks>().To<InstallationCallbackHandler>();
        }
    }
}