using System;

namespace OnlineCatalog.Web.Main.Models.Groups
{
    public class ShopUserViewModel
    {
        public Guid ShopGuid { get; set; }

        public string ShopName { get; set; }

        public Guid UserIdentity { get; set; }

        public bool IsShopAssigned { get; set; }
    }
}