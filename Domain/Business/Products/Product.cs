using System;
using System.Collections.Generic;
using Business.Administration;

namespace Business.Products
{
    public class Product : BaseDomainObject, IAuditable
    {
        private IEnumerable<ProductCategory> _categories;
         
        public Product()
        {
        }

        public virtual IEnumerable<ProductCategory> Categories
        {
            get { return _categories ?? (_categories = new List<ProductCategory>()); }
            set { _categories = value; }
        }

        public virtual string ProductName { get; set; }

        public virtual decimal DefaultPrice { get; set; }

        public virtual int Tax { get; set; }

        public virtual byte[] ProductImage { get; set; }

        public virtual string Description { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsDeleted { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

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
