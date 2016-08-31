using System.Collections.Generic;

namespace Business.Products
{
    public class ProductCategory : BaseDomainObject
    {
        internal ProductCategory()
        { }

        public ProductCategory(string categoryName) : this()
        {
            CategoryName = categoryName;
        }

        public virtual string CategoryName { get; protected set; }

        public virtual IEnumerable<Product> AssignedProducts { get; set; } 
    }
}
