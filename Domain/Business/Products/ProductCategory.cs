using System;
using System.Collections.Generic;
using Business.Groups;

namespace Business.Products
{
    public class ProductCategory : BaseDomainObject, IAuditable
    {
        internal ProductCategory()
        { }

        public ProductCategory(string categoryName) : this()
        {
            CategoryName = categoryName;
        }

        public virtual string CategoryName { get; set; }

        public virtual IEnumerable<Product> AssignedProducts { get; set; }

        public DateTime Created
        {
            get;
            private set;
        }

        public DateTime Updated
        {
            get;
            private set;
        }

        public virtual Shop ProductCategoryShop { get; set; }

        public void SetCreatedDate(DateTime dateTime)
        {
            if (Created.Equals(DateTime.MinValue))
            {
                Created = dateTime;
            }
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            Updated = dateTime;
        }
    }
}
