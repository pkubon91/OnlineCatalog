using System;
using Business.DataAccess.Group;
using Business.DataAccess.Products;
using Business.Groups;
using Business.Products;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductCategoryService.Tests.Unit
{
    [TestFixture]
    public class ProductCategoryServiceTests
    {
        [Test]
        public void WhenProductCategoryRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductCategoryService(null, Mock.Of<IShopRepository>()));
        }

        [Test]
        public void WhenShopServiceIsNullThenThrowArgumentnullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), null));
        }

        [Test]
        public void WhenProductCategoryIsNullThenNotSuccessfullStatusIsReturned()
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepository>());

            ServiceActionResult result = service.AddProductCategory(null);

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Product category cannot be null");
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenProductCategoryNameIsNullOrEmptyThenNotSuccessfullStatusIsReturned(string categoryName)
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepository>());

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = categoryName });

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Category name cannot be null or empty");
        }

        [Test]
        public void WhenShopGuidIsEmptyGuidThenNotSuccessfullActionIsReturned()
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepository>());

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
                    Mock.Of<IShopRepository>(r => r.GetShopById(shopGuid) == new Shop() {UniqueId = shopGuid}));

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = shopGuid });

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
            result.Reason.Should().Be("Product category already exist");
        }

        [Test]
        public void WhenShopDoesntExistThenNotSuccessfullActionResultIsReturned()
        {
            Guid shopGuid = Guid.NewGuid();
            var shopRepositoryMock = new Mock<IShopRepository>();
            shopRepositoryMock.Setup(r => r.GetShopById(shopGuid)).Returns((Shop) null);
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
            var shopRepositoryMock = new Mock<IShopRepository>();
            shopRepositoryMock.Setup(r => r.GetShopById(shopId)).Returns(new Shop() {UniqueId = shopId});
            ProductCategoryService service = new ProductCategoryService(repositoryMock.Object, shopRepositoryMock.Object);

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() { CategoryName = "testName", ShopGuid = shopId });

            result.ShouldBeEquivalentTo(ServiceActionResult.Successfull);
            repositoryMock.Verify(r => r.AddToDatabase(It.IsAny<Business.Products.ProductCategory>()), Times.Once());
        }

        [Test]
        public void WhenProductCategoryIsNullThenEditProductCategoryReturnNotSuccessfullResult()
        {
            var service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>(), Mock.Of<IShopRepository>());

            var actionResult = service.EditProductCategory(null);

            actionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenProductCategoryNotFoundThenEditProductCategoryReturnNotSuccessfullResult()
        {
            var productCategoryGuid = Guid.Parse("{02C0CB50-DB9F-45EC-9C67-1D4550AA86EC}");
            var productCategoryRepository = Mock.Of<IProductCategoryRepository>();
            Mock.Get(productCategoryRepository).Setup(r => r.GetProductCategory(productCategoryGuid)).Returns((ProductCategory) null);
            var service = new ProductCategoryService(productCategoryRepository, Mock.Of<IShopRepository>());

            var actionResult = service.EditProductCategory(new ProductCategoryDto() {ProductCategoryGuid = productCategoryGuid });

            actionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenProductCategoryWithDefinedNameAlreadyExistsThenEditProductCategoryReturnNotSuccessfull()
        {
            var productCategoryGuid = Guid.Parse("{02C0CB50-DB9F-45EC-9C67-1D4550AA86EC}");
            var productCategory = new ProductCategory("Name");
            var productCategoryRepository = Mock.Of<IProductCategoryRepository>(r => r.GetProductCategory(productCategoryGuid) == productCategory && r.GetProductCategoryByName(It.IsAny<Guid>(), "Name") == productCategory);
            var service = new ProductCategoryService(productCategoryRepository, Mock.Of<IShopRepository>());

            var actionResult = service.EditProductCategory(new ProductCategoryDto() { ProductCategoryGuid = productCategoryGuid, CategoryName = "Name"});

            actionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenProductCategoryIsUniqueThenEditProductCategoryReturnSuccessfull()
        {
            var productCategoryGuid = Guid.Parse("{02C0CB50-DB9F-45EC-9C67-1D4550AA86EC}");
            var productCategory = new ProductCategory("Name");
            var productCategoryRepository = Mock.Of<IProductCategoryRepository>(r => r.GetProductCategory(productCategoryGuid) == productCategory);
            Mock.Get(productCategoryRepository).Setup(r => r.GetProductCategoryByName(It.IsAny<Guid>(), "Name")).Returns((ProductCategory) null);
            var service = new ProductCategoryService(productCategoryRepository, Mock.Of<IShopRepository>());

            var actionResult = service.EditProductCategory(new ProductCategoryDto() { ProductCategoryGuid = productCategoryGuid, CategoryName = "Name" });

            actionResult.Should().Be(ServiceActionResult.Successfull);
        }

        [Test]
        public void WhenExceptionThrownThenReturnWithExceptionResult()
        {
            var productCategoryRepository = Mock.Of<IProductCategoryRepository>();
            Mock.Get(productCategoryRepository).Setup(r => r.GetProductCategory(It.IsAny<Guid>())).Throws<Exception>();
            var service = new ProductCategoryService(productCategoryRepository, Mock.Of<IShopRepository>());

            var actionResult = service.EditProductCategory(new ProductCategoryDto() { CategoryName = "Name" });

            actionResult.Status.Should().Be(ActionStatus.WithException);
        }
    }
}
