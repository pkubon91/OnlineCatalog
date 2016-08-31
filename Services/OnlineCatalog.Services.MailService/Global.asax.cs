using System;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OnlineCatalog.Services.MailService.MailClient;
using OnlineCatalog.Services.MailService.MessageBuilder;

namespace OnlineCatalog.Services.MailService
{
    public class Global : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();
            _container.AddFacility<WcfFacility>()
                .Register(Component.For<IClient>().ImplementedBy<Client>())
                .Register(Component.For<IMessageBuilderFactory>().ImplementedBy<MessageBuilderFactory>())
                .Register(Component.For<IMailService>().ImplementedBy<MailService>());
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
//            if(_container != null) _container.Dispose();
        }
    }
}