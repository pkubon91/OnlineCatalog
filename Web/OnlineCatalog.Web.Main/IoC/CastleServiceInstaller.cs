using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OnlineCatalog.Web.Main.BasketClient;
using OnlineCatalog.Web.Main.BasketRepositoryClient;
using OnlineCatalog.Web.Main.LoginClient;
using OnlineCatalog.Web.Main.OrderClient;
using OnlineCatalog.Web.Main.ProductAdministrationClient;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;
using OnlineCatalog.Web.Main.ProductCategoryServiceClient;
using OnlineCatalog.Web.Main.ProductRepositoryClient;
using OnlineCatalog.Web.Main.RegistrationClient;
using OnlineCatalog.Web.Main.ShopConfigurationClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.IoC
{
    public class CastleServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILoginService>().ImplementedBy<LoginServiceClient>())
                .Register(Component.For<IShopRepositoryService>().ImplementedBy<ShopRepositoryServiceClient>())
                .Register(Component.For<IRegisterService>().ImplementedBy<RegisterServiceClient>())
                .Register(Component.For<IShopConfigurationService>().ImplementedBy<ShopConfigurationServiceClient>())
                .Register(Component.For<IProductCategoryRepositoryService>().ImplementedBy<ProductCategoryRepositoryServiceClient>())
                .Register(Component.For<IProductCategoryService>().ImplementedBy<ProductCategoryServiceClient.ProductCategoryServiceClient>())
                .Register(Component.For<IProductRepositoryService>().ImplementedBy<ProductRepositoryServiceClient>())
                .Register(Component.For<IProductAdministrationService>().ImplementedBy<ProductAdministrationServiceClient>())
                .Register(Component.For<IBasketService>().ImplementedBy<BasketServiceClient>())
                .Register(Component.For<IBasketRepositoryService>().ImplementedBy<BasketRepositoryServiceClient>())
                .Register(Component.For<IOrderService>().ImplementedBy<OrderServiceClient>());
        }
    }
}