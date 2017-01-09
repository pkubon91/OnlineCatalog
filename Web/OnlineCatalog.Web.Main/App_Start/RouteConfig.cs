using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineCatalog.Web.Main
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ShopActivation",
                url: "Shop/{action}/{shopGuid}",
                defaults: new { controller = "Shop", action = "{action}", shopGuid = Guid.Empty });
            routes.MapRoute(
                name: "AdminShopList",
                url: "Shop/AdminShopList/",
                defaults: new {controller = "Shop", action = "AdminShopList"});
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "LoginView", action = "Login", id = UrlParameter.Optional });
        }
    }
}
