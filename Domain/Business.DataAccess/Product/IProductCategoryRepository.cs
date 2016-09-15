using Business.Products;

namespace Business.DataAccess.Product
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        ProductCategory GetProductCategoryById(int id);

        ProductCategory EditProductCategory(int productCategoryId, ProductCategory productCategory);

        void RemoveProductCategory(int id);
    }
}
