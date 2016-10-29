using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OnlineCatalog.Web.Main.LoginClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.IoC
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private IWindsorContainer _container = new WindsorContainer();
        
        public WindsorDependencyResolver()
        {
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            yield return _container.Resolve(serviceType);
        }

        private void AddBindings()
        {
            _container
//                .Register(Component.For<IControllerFactory>().ImplementedBy())
                .Register(Component.For<ILoginService>().ImplementedBy<LoginServiceClient>())
                .Register(Component.For<IShopRepositoryService>().ImplementedBy<ShopRepositoryServiceClient>());
        }
    }
}