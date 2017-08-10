using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService
{
    [ServiceContract(Namespace = "https://online.catalog.com")]
    public interface IProductAdministrationService
    {
        [OperationContract]
        ServiceActionResult AddProduct(ProductDto product);

        [OperationContract]
        ServiceActionResult EditProduct(ProductDto product);

        [OperationContract]
        ServiceActionResult RemoveProduct(Guid productGuid);
    }
}
