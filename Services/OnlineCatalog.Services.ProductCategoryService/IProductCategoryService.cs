using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductCategoryService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IProductCategoryService
    {
        [OperationContract]
        ServiceActionResult AddProductCategory(ProductCategoryDto productCategory);

        [OperationContract]
        ServiceActionResult RemoveProductCategory(Guid productCategoryUniqueId);

        [OperationContract]
        ServiceActionResult EditProductCategory(ProductCategoryDto productCategory);
    }
}
