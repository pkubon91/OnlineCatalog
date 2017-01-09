using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.Common.Authentication;
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
            container.Install(new CastleServiceInstaller(), new CastleApplicationInstaller(), new CastleCustomDependenciesInstaller());

            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));

            RegisterUserRoles();
        }

        private void RegisterUserRoles()
        {
            if (!Roles.RoleExists(UserRoles.Client)) Roles.CreateRole(UserRoles.Client);
            if (!Roles.RoleExists(UserRoles.Administrator)) Roles.CreateRole(UserRoles.Administrator);
            if (!Roles.RoleExists(UserRoles.ShopAdministrator)) Roles.CreateRole(UserRoles.ShopAdministrator);
        }
    }
}
