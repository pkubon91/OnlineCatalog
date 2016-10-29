using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OnlineCatalog.Web.Main.LoginClient;
using OnlineCatalog.Web.Main.RegistrationClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.IoC
{
    public class CastleServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILoginService>().ImplementedBy<LoginServiceClient>())
                .Register(Component.For<IShopRepositoryService>().ImplementedBy<ShopRepositoryServiceClient>())
                .Register(Component.For<IRegisterService>().ImplementedBy<RegisterServiceClient>());
        }
    }
}