using System;
using Business.DataAccess.Group;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Mappings;

namespace OnlineCatalog.Services.ShopService
{
    public class ShopConfigurationService : IShopConfigurationService
    {
        private readonly IShopRepository _shopRepository;

        public ShopConfigurationService(IShopRepository shopRepository)
        {
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            _shopRepository = shopRepository;
        }

        public ServiceActionResult AddNewShop(ShopDto shop)
        {
            if(shop == null) throw new ArgumentNullException(nameof(shop));
            
            try
            {
                var checkShop = _shopRepository.GetShopByName(shop.Name);
                if (checkShop != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Shop with the same name already exist");

                _shopRepository.AddToDatabase(shop.Map());
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}