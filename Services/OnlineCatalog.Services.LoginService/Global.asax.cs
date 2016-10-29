using System;
using Business.DataAccess.Administration;
using Business.NHibernate;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace OnlineCatalog.Services.LoginService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _dependencyContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            _dependencyContainer = new WindsorContainer();
            _dependencyContainer.AddFacility<WcfFacility>()
                .Register(Component.For<ISessionProvider>().ImplementedBy<SessionFactory>())
                .Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>())
                .Register(Component.For<ILoginService>().ImplementedBy<LoginService>());
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