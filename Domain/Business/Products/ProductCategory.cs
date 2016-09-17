using System;
using System.Collections.Generic;

namespace Business.Products
{
    public class ProductCategory : BaseDomainObject, IAuditable
    {
        private DateTime _createdDateTime;
        private DateTime _updatedDateTime;

        internal ProductCategory()
        { }

        public ProductCategory(string categoryName) : this()
        {
            CategoryName = categoryName;
        }

        public virtual string CategoryName { get; protected set; }

        public virtual IEnumerable<Product> AssignedProducts { get; set; }

        public DateTime Created => _createdDateTime;

        public DateTime Updated => _updatedDateTime;

        public void SetCreatedDate(DateTime dateTime)
        {
            if (_createdDateTime.Equals(DateTime.MinValue))
            {
                _createdDateTime = dateTime;
            }
        }

        public void SetUpdatedDate(DateTime dateTime)
        {
            _updatedDateTime = dateTime;
        }
    }
}
