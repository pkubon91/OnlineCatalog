using System;
using System.Collections.Generic;
using Business.Administration;
using Business.Groups;

namespace Business.DataAccess.Group
{
    public interface IShopRepository : IRepository<Shop>
    {
        IEnumerable<Shop> GetAllShops();

        Shop GetShopByName(string shopName);

        Shop GetShopById(Guid uniqueId);

        void UpdateShop(Shop shop);

        void AssignUser(Shop shop, User user);
    }
}
