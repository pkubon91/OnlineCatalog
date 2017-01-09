using System;
using Business.DataAccess.Group;
using Business.Groups;
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
            if(shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Shop cannot be null");
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

        public ServiceActionResult ShopActivation(Guid shopGuid, bool isActive)
        {
            try
            {
                var shop = _shopRepository.GetShopById(shopGuid);
                if (shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"Shop {shopGuid} does not exist");
                shop.SetActive(isActive);
                _shopRepository.UpdateShop(shop);

                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        public ServiceActionResult EditShop(ShopDto shop)
        {
            if (shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Shop is empty");
            try
            {
                Shop editedShop = _shopRepository.GetShopById(shop.ShopGuid);
                if(editedShop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"Shop {shop.Name} does not exist in the system");

                editedShop.Name = shop.Name;
                editedShop.Address.BuildingNumber = shop.Address.BuildingNumber;
                editedShop.Address.City = shop.Address.City;
                editedShop.Address.Email = shop.Address.Email;
                editedShop.Address.PhoneNumber = shop.Address.PhoneNumber;
                editedShop.Address.Street = shop.Address.Street;

                _shopRepository.UpdateShop(editedShop);

                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}