using System;
using System.Collections.Generic;
using Business.Groups;

namespace Business.DataAccess.Group
{
    public interface IShopRepository : IRepository<Shop>
    {
        IEnumerable<Shop> GetAllActiveShops();

        Shop GetShopByName(string shopName);

        Shop GetShopById(Guid uniqueId);
    }
}
