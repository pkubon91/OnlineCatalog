using System;
using Business.DataAccess.Administration;
using Business.DataAccess.Orders;
using Business.DataAccess.Products;
using Business.NHibernate;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OnlineCatalog.Services.OrderService.MailClient;

namespace OnlineCatalog.Services.OrderService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _dependencyContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            _dependencyContainer = new WindsorContainer();
            _dependencyContainer.AddFacility<WcfFacility>()
                .Register(Component.For<ISessionProvider>().ImplementedBy<SessionFactory>())
                .Register(Component.For<IBasketRepository>().ImplementedBy<BasketRepository>())
                .Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>())
                .Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>())
                .Register(Component.For<IBasketRepositoryService>().ImplementedBy<BasketRepositoryService>())
                .Register(Component.For<IOrderService>().ImplementedBy<OrdersService>())
                .Register(Component.For<IBasketService>().ImplementedBy<BasketService>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            _dependencyContainer?.Dispose();
        }
    }
}