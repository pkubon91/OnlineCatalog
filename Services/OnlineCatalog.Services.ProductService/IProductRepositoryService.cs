using System;
using System.Collections.Generic;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IProductRepositoryService
    {
        [OperationContract]
        IEnumerable<ProductDto> GetShopProducts(Guid shopGuid);

        [OperationContract]
        ProductDto GetProduct(Guid productGuid);
    }
}
