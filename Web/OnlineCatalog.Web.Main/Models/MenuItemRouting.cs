namespace OnlineCatalog.Web.Main.Models
{
    public class MenuItemRouting
    {
        public static readonly MenuItemRouting ProductCategories = new MenuItemRouting("Product Categories", "AdminShopProductCategories");
        public static readonly MenuItemRouting Product = new MenuItemRouting("Products", "AdminShopProduct");
        public static readonly MenuItemRouting Order = new MenuItemRouting("Orders", "AdminShopOrders");
        public static readonly MenuItemRouting User = new MenuItemRouting("Users", "AdminShopUsers");

        public static readonly MenuItemRouting ClientShops = new MenuItemRouting("Shops", "Shops");
        public static readonly MenuItemRouting ClientShopProducts = new MenuItemRouting("Products", "ShopProducts");
        public static readonly MenuItemRouting ClientShopBasket = new MenuItemRouting("Basket", "ShopBasket");
        public static readonly MenuItemRouting ClientOrders = new MenuItemRouting("Orders", "ClientOrders");

        private MenuItemRouting(string menuItem, string routeTo)
        {
            MenuItem = menuItem;
            RouteTo = routeTo;
        }

        public string MenuItem { get; private set; }

        public string RouteTo { get; private set; }
    }
}