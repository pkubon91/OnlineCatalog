using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using OnlineCatalog.Web.Main.IoC;

namespace OnlineCatalog.Web.Main
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            var container = new WindsorContainer();
            container.Install(new CastleServiceInstaller(), new CastleApplicationInstaller());

            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));
        }
    }
}
