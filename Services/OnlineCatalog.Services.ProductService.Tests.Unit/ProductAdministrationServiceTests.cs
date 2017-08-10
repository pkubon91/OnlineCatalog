using System;
using Business.DataAccess.Administration;
using Business.DataAccess.Products;
using Business.Products;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService.Tests.Unit
{
    [TestFixture]
    public class ProductAdministrationServiceTests
    {
        [Test]
        public void WhenProductRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductAdministrationService(null, Mock.Of<IUserRepository>(), Mock.Of<IProductCategoryRepository>()));
        }

        [Test]
        public void WhenProductIsNullThenReturnNotSuccessfullActionResult()
        {
            var system = new ProductAdministrationService(Mock.Of<IProductRepository>(), Mock.Of<IUserRepository>(), Mock.Of<IProductCategoryRepository>());

            var actionResult = system.AddProduct(null);

            Assert.AreEqual(ActionStatus.NotSuccessfull, actionResult.Status);
        }

        [Test]
        public void WhenProductAddedToRepositoryThenServiceActionResultSuccessfullReturned()
        {
            var productRepository = new Mock<IProductRepository>();
            var system = new ProductAdministrationService(productRepository.Object, Mock.Of<IUserRepository>(), Mock.Of<IProductCategoryRepository>());

            var actionResult = system.AddProduct(new ProductDto()
            {
                Description = "Test",
                DefaultPrice = 100m,
//                ProductCategories = new []{new ProductCategoryDto()},
                ProductName = "TestName",
            });

            Assert.AreEqual(ServiceActionResult.Successfull, actionResult);
        }

        [Test]
        public void WhenExceptionThrownFromProductRepositoryThenReturnNotSuccessfullActionResult()
        {
            var productDto = new ProductDto();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(r => r.AddToDatabase(It.IsAny<Product>())).Throws<Exception>();
            var system = new ProductAdministrationService(productRepository.Object, Mock.Of<IUserRepository>(), Mock.Of<IProductCategoryRepository>());

            var actionResult = system.AddProduct(productDto);

            Assert.AreEqual(ActionStatus.NotSuccessfull, actionResult.Status);
        }
    }
}
