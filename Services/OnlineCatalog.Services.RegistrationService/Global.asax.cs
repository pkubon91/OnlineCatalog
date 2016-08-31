using System;
using Business.DataAccess.Administration;
using Business.NHibernate;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.Validations;
//using OnlineCatalog.Services.MailService;
using OnlineCatalog.Services.RegistrationService.Validations;

namespace OnlineCatalog.Services.RegistrationService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>().Register(
                    Component.For<IValidator<UserDto>>().ImplementedBy<UserRegistrationValidator>(),
                    Component.For<ISessionProvider>().ImplementedBy<SessionFactory>(),
                    Component.For<IUserRepository>().ImplementedBy<UserRepository>(),
                    //Component.For<IMailService>().ImplementedBy<MailServiceClient>(),
                    Component.For<IRegisterService>().ImplementedBy<RegisterService>());
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
            if (_container != null) _container.Dispose();
        }
    }
}