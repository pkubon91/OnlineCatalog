using System;
using System.Linq;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.DataAccess.Orders;
using Business.DataAccess.Products;
using Business.Groups;
using Business.Orders;
using Business.Products;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Services.OrderService.MailClient;

namespace OnlineCatalog.Services.OrderService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMailService _mailClient;
        private readonly IUserRepository _userRepository;

        public BasketService(IBasketRepository basketRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));

            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public ServiceActionResult AddProductToBasket(Guid shopGuid, Guid productGuid, string loginUser)
        {
            if (loginUser == null) return new ServiceActionResult(ActionStatus.NotSuccessfull,  "User with empty login does not exists");
            try
            {
                User user = _userRepository.GetUserByLogin(loginUser);
                if(user == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"User with {loginUser} does not exists");

                Basket basket = _basketRepository.GetBasketForShopAndOwner(shopGuid, user.UniqueId, BasketState.Basket) ??
                                CreateNewBasket(shopGuid, user);

                Product product = _productRepository.GetProductById(productGuid);
                if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product with specified guid doesn't exist");

                basket.AddProduct(product);
                _basketRepository.UpdateBasket(basket);

                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        public ServiceActionResult RemoveProductFromBasket(Guid basketGuid, Guid productGuid)
        {
            try
            {
                Basket basket = _basketRepository.GetBasketByUniqueId(basketGuid);
                if(basket == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Specified basket not found");

                Product productBasket = basket.BasketProducts.FirstOrDefault(p => p.UniqueId == productGuid);
                if(productBasket == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Specified product is already removed from the basket");

                basket.BasketProducts.Remove(productBasket);
                _basketRepository.UpdateBasket(basket);

                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        private Basket CreateNewBasket(Guid shopGuid, User user)
        {
            Basket basket = new Basket
            {
                Owner = user,
                BasketShop = new Shop { UniqueId = shopGuid },
                State = BasketState.Basket
            };
            _basketRepository.AddToDatabase(basket);

            return basket;
        }
    }
}