using System;
using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Group;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Mappings;

namespace OnlineCatalog.Services.ShopService
{
    public class ShopRepositoryService : IShopRepositoryService
    {
        private readonly IShopRepository _shopRepository;

        public ShopRepositoryService(IShopRepository shopRepository)
        {
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            _shopRepository = shopRepository;
        }

        public IEnumerable<ShopDto> GetAllActiveShops()
        {
            var allShops = _shopRepository.GetAllActiveShops();
            if (allShops == null) return Enumerable.Empty<ShopDto>();
            return allShops.Select(s => s.Map());
        }

        public ShopDto GetShopByName(string shopName)
        {
            if(shopName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(shopName));

            var shopByName = _shopRepository.GetShopByName(shopName);
            if (shopByName == null) return ShopDto.EmptyShop;
            return shopByName.Map();
        }

        public ShopDto GetShopByUniqueId(Guid uniqueId)
        {
            var shop = _shopRepository.GetShopById(uniqueId);
            if(shop == null) return ShopDto.EmptyShop;
            return shop.Map();
        }
    }
}