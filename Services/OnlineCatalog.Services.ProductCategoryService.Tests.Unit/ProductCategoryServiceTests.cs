using System;
using Business.DataAccess.Product;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Services.ProductCategoryService.ShopService;

namespace OnlineCatalog.Services.ProductCategoryService.Tests.Unit
{
    [TestFixture]
    public class ProductCategoryServiceTests
    {
        [Test]
        public void WhenProductCategoryRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductCategoryService(null, Mock.Of<IShopRepositoryService>()));
        }

        [Test]
        public void WhenShopServiceIsNullThenThrowArgumentnullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), null));
        }

        [Test]
        public void WhenProductCategoryIsNullThenNotSuccessfullStatusIsReturned()
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepositoryService>());

            ServiceActionResult result = service.AddProductCategory(null);

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Product category cannot be null");
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenProductCategoryNameIsNullOrEmptyThenNotSuccessfullStatusIsReturned(string categoryName)
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepositoryService>());

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = categoryName });

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Category name cannot be null or empty");
        }

        [Test]
        public void WhenShopGuidIsEmptyGuidThenNotSuccessfullActionIsReturned()
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepositoryService>());

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = Guid.Empty });

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Shop guid cannot be empty");
        }

        [Test]
        public void WhenProductCategoryWithDefinedGuidAlreadyExistsThenNotSuccessfullStatusIsReturned()
        {
            Guid shopGuid = Guid.NewGuid();
            ProductCategoryService service =
                new ProductCategoryService(
                    Mock.Of<IProductCategoryRepository>(
                        r => r.GetProductCategoryByName(shopGuid, "testName") == new Business.Products.ProductCategory("testName")),
                    Mock.Of<IShopRepositoryService>(r => r.GetShopByUniqueId(shopGuid) == new ShopDto() {ShopGuid = shopGuid}));

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = shopGuid });

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Product category already exist");
        }

        [Test]
        public void WhenShopDoesntExistThenNotSuccessfullActionResultIsReturned()
        {
            Guid shopGuid = Guid.NewGuid();
            var shopRepositoryMock = new Mock<IShopRepositoryService>();
            shopRepositoryMock.Setup(r => r.GetShopByUniqueId(shopGuid)).Returns((ShopDto) null);
            ProductCategoryService service =
                new ProductCategoryService(
                    Mock.Of<IProductCategoryRepository>(
                        r => r.GetProductCategoryByName(shopGuid, "testName") == new Business.Products.ProductCategory("testName")),
                    shopRepositoryMock.Object);

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = shopGuid });
            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Provided shop doesn't exist");
        }

        [Test]
        public void WhenProductCategoryIsSuccessfullyAddedThenSuccessfullServiceActionResultIsReturned()
        {
            var shopId = Guid.NewGuid();
            var repositoryMock = new Mock<IProductCategoryRepository>();
            repositoryMock.Setup(r => r.GetProductCategoryByName(shopId, "testName")).Returns((Business.Products.ProductCategory)null);
            var shopRepositoryMock = new Mock<IShopRepositoryService>();
            shopRepositoryMock.Setup(r => r.GetShopByUniqueId(shopId)).Returns(new ShopDto() {ShopGuid = shopId});
            ProductCategoryService service = new ProductCategoryService(repositoryMock.Object, shopRepositoryMock.Object);

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = shopId });

            result.ShouldBeEquivalentTo(ServiceActionResult.Successfull);
            repositoryMock.Verify(r => r.AddToDatabase(It.IsAny<Business.Products.ProductCategory>()), Times.Once());
        }
    }
}
