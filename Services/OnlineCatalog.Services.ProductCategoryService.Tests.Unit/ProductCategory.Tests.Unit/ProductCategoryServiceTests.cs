using System;
using Business.DataAccess.Product;
using Business.Groups;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Services.ProductCategoryService;
using ProductCategory = Business.Products.ProductCategory;

namespace ProductCategory.Tests.Unit
{
    [TestFixture]
    public class ProductCategoryServiceTests
    {
        [Test]
        public void WhenProductCategoryRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductCategoryService(null));
        }

        [Test]
        public void WhenProductCategoryIsNullThenNotSuccessfullStatusIsReturned()
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>());

            ServiceActionResult result = service.AddProductCategory(null);

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenProductCategoryNameIsNullOrEmptyThenNotSuccessfullStatusIsReturned(string categoryName)
        {
            ProductCategoryService service = new ProductCategoryService(Mock.Of<IProductCategoryRepository>());

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() {CategoryName = categoryName});

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenProductCategoryWithDefinedGuidAlreadyExistsThenNotSuccessfullStatusIsReturned()
        {
            Guid shopGuid = new Guid();
            ProductCategoryService service =
                new ProductCategoryService(
                    Mock.Of<IProductCategoryRepository>(
                        r => r.GetProductCategoryByName(shopGuid, "testName") == new Business.Products.ProductCategory("testName")));

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() {CategoryName = "testName", ShopGuid = shopGuid});

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenProductCategoryIsSuccessfullyAddedThenSuccessfullServiceActionResultIsReturned()
        {
            var shopId = Guid.NewGuid();
            var repositoryMock = new Mock<IProductCategoryRepository>();
            repositoryMock.Setup(r => r.GetProductCategoryByName(shopId, "testName")).Returns((Business.Products.ProductCategory) null);
            ProductCategoryService service = new ProductCategoryService(repositoryMock.Object);

            ServiceActionResult result = service.AddProductCategory(new ProductCategoryDto() {CategoryName = "testName", ShopGuid = shopId});

            result.ShouldBeEquivalentTo(ServiceActionResult.Successfull);
            repositoryMock.Verify(r => r.AddToDatabase(It.IsAny<Business.Products.ProductCategory>()), Times.Once());
        }
    }
}
