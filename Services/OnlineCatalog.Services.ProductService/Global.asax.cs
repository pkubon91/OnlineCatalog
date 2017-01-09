using System;
using Business.DataAccess.Products;
using Business.NHibernate;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace OnlineCatalog.Services.ProductService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            if(_container == null) _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>()
                .Register(Component.For<ISessionProvider>().ImplementedBy<SessionFactory>())
                .Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>())
                .Register(Component.For<IProductAdministrationService>().ImplementedBy<ProductAdministrationService>());
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
            _container?.Dispose();
        }
    }
}