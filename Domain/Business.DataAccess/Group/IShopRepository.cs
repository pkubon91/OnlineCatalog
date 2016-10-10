using System;
using Business.Groups;

namespace Business.DataAccess.Group
{
    public interface IShopRepository : IRepository<Shop>
    {
        Shop GetShopByName(string shopName);

        Shop GetShopById(Guid uniqueId);
    }
}
