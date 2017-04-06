namespace OnlineCatalog.Web.Main.Models
{
    public class MenuItemRouting
    {
        public static readonly MenuItemRouting ProductCategories = new MenuItemRouting("Product Categories", "AdminShopProductCategories");
        public static readonly MenuItemRouting Product = new MenuItemRouting("Products", "AdminShopProduct");
        public static readonly MenuItemRouting Order = new MenuItemRouting("Orders", "AdminShopOrders");
        public static readonly MenuItemRouting User = new MenuItemRouting("Users", "AdminShopUsers");

        private MenuItemRouting(string menuItem, string routeTo)
        {
            MenuItem = menuItem;
            RouteTo = routeTo;
        }

        public string MenuItem { get; private set; }

        public string RouteTo { get; private set; }
    }
}