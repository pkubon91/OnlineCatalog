using System;
using System.Collections.Generic;
using Business.Products;
using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Common.DataContracts.Tests.Unit.Mappings.ProductMappings
{
    [TestFixture]
    public class ProductCategoryMappertTests
    {
        [Test]
        public void WhenProductCategoryIsNullThenThrowArgumentNullException()
        {
            ProductCategory productCategory = null;

            Assert.Throws<ArgumentNullException>(() => productCategory.Map());
        }

        [Test]
        public void AllPropertiesAreMappedCorrectlyFromProductCategoryToProductCategoryDto()
        {
            ProductCategory productCategory = new ProductCategory("categoryname")
            {
                AssignedProducts = new List<Product> { new Product() { ProductName = "name"} },
                UniqueId = Guid.NewGuid()
            };
            var createdDate = new DateTime(2016, 9, 2);
            productCategory.SetCreatedDate(createdDate);
            var updatedDate = new DateTime(2016, 10, 3);
            productCategory.SetUpdatedDate(updatedDate);

            ProductCategoryDto productCategoryDto = productCategory.Map();

            productCategoryDto.CategoryName.Should().Be(productCategory.CategoryName);
            productCategoryDto.CreatedDateTime.Should().Be(createdDate);
            productCategoryDto.UpdatedDateTime.Should().Be(updatedDate);
            productCategoryDto.AssignedProducts.Should().HaveCount(1).And.Contain(x => x.ProductName == "name");
        }
    }
}
