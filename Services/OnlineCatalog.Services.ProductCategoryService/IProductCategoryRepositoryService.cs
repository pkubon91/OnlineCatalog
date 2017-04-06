using System;
using System.Collections.Generic;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductCategoryService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IProductCategoryRepositoryService
    {
        [OperationContract]
        IEnumerable<ProductCategoryDto> GetProductCategories(Guid shopGuid);

        [OperationContract]
        ProductCategoryDto GetProductCategory(Guid productCategoryGuid);
    }
}