using System;
using Business.DataAccess.Orders;
using Business.DataAccess.Products;
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

        public BasketService(IBasketRepository basketRepository, IProductRepository productRepository, IMailService mailClient)
        {
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            if (mailClient == null) throw new ArgumentNullException(nameof(mailClient));

            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _mailClient = mailClient;
        }

        public ServiceActionResult AddProductToBasket(Guid basketGuid, Guid productGuid)
        {
            try
            {
                Basket basket = _basketRepository.GetBasketByUniqueId(basketGuid);
                if(basket == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Basket with specified guid doesn't exist");

                Product product = _productRepository.GetProductById(productGuid);
                if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product with specified guid doesn't exist");

                _basketRepository.AssignProduct(basket, product);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        public ServiceActionResult RemoveProductFromBasket(Guid basGuid, Guid productGuid)
        {
            throw new NotImplementedException();
        }
    }
}