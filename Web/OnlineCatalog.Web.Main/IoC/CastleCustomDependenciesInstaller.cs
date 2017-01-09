using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OnlineCatalog.Web.Main.Common.Authentication;

namespace OnlineCatalog.Web.Main.IoC
{
    public class CastleCustomDependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if(container == null) throw new ArgumentNullException(nameof(container));
            container.Register(Component.For<IRoleAuthenticationRouterFactory>().ImplementedBy<RoleAuthenticationRouterFactory>());
        }
    }
}